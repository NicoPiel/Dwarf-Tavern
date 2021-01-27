using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Messages
{
    public class MessageSystemHandler : MonoBehaviour
    {
        public MessageSystem messageSystem;
        public GameObject debugUI;
        public bool resetMessageSystem = true;
        private List<string> _cancelledTasks = new List<string>();

        private int _randomGameSeed;
        public static MessageSystemHandler Instance
        {
            get;
            private set;
        }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                if (resetMessageSystem || !ES3.KeyExists("msg_randomSeed"))
                {
                    _randomGameSeed = new System.Random().Next();
                }
                else
                {
                    _randomGameSeed = ES3.Load<int>("msg_randomSeed");
                }

                if (!resetMessageSystem && ES3.KeyExists("msg_cancelledTasks"))
                {
                    _cancelledTasks = ES3.Load<List<string>>("msg_cancelledTasks");
                }
                else
                {
                    _cancelledTasks.AddRange(messageSystem.tasks.Where(task => task.defaultCancelled).Select(task => task.name));
                }

                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        public void Start()
        {
            EventHandler.onAfterHourSceneLoaded.AddListener(Save);
        }

        public void Save()
        {
            ES3.Save("msg_cancelledTasks", _cancelledTasks);
            ES3.Save("msg_randomSeed", _randomGameSeed);
        }

        private int CreateSeed(int localSeed)
        {
            return _randomGameSeed ^ localSeed;
        }

        public Dictionary<MessageTask, Message> GetMessagesForDay(int day)
        {
            Dictionary<MessageTask, Message> ret = new Dictionary<MessageTask, Message>();
            
            //Filter out any cancelled tasks
            List<MessageTask> tasks = messageSystem.tasks.Where(task => !IsCancelled(task)).ToList();
            
            //Add all FixedDayTask messages for this day
            tasks.OfType<FixedDayTask>().Where(task => task.day == day).Select(task => new KeyValuePair<MessageTask, Message>(task, task.message)).ToList().ForEach(pair => ret[pair.Key] = pair.Value);
            
            //Add all repeating messages for this day
            List<RepeatingMessageTask> repeatingTasks = tasks
                .OfType<RepeatingMessageTask>()
                .Where(task =>
                    day >= task.firstDay &&
                    (day - task.firstDay) % task.period == 0 &&
                    ((day - task.firstDay) / task.period <= task.repetitions || task.repetitions < 0)
                    )
                .ToList();
            
            repeatingTasks.Where(task => task.GetType() == typeof(RepeatingMessageTask)).Select(task => new KeyValuePair<MessageTask, Message>(task, task.message)).ToList().ForEach(pair => ret[pair.Key] = pair.Value);
            
            repeatingTasks
                .OfType<RepeatingMultiMessageTask>()
                .Select(task =>
                {
                    int iteration = (day - task.firstDay) / task.period;
                    switch (task.messageSelection)
                    {
                        case RepeatingMultiMessageTask.MessageSelection.Sequential:
                            return new KeyValuePair<MessageTask, Message>(task, task.messages[iteration % task.messages.Count]);
                        case RepeatingMultiMessageTask.MessageSelection.Random:
                            System.Random rand = new System.Random(Math.Abs(CreateSeed(task.RandomSeed()) - iteration));
                            return new KeyValuePair<MessageTask, Message>(task, task.messages[rand.Next(0, task.messages.Count)]);
                        default:
                            return new KeyValuePair<MessageTask, Message>(task, task.message);
                    }
                }).ToList().ForEach(entry => ret.Add(entry.Key, entry.Value));
            
            //Add random day messages
            tasks
                .OfType<RandomDayTask>()
                .Where(task => task.fromDayInclusive <= day && (day <= task.toDayInclusive || task.toDayInclusive <= 0))
                .Where(task =>
                {
                    System.Random rand;
                    if (task.toDayInclusive > 0)
                    {
                        rand = new System.Random(CreateSeed(task.RandomSeed()));
                        int randomDay = rand.Next(task.fromDayInclusive, task.toDayInclusive + 1);
                        return randomDay == day;
                    }
                    rand = new System.Random(Math.Abs(CreateSeed(task.RandomSeed()) - day));
                    return rand.NextDouble() <= task.chance;
                })
                .Select(task => new KeyValuePair<MessageTask, Message>(task, task.message)).ToList().ForEach(entry => ret.Add(entry.Key, entry.Value));
            
            return ret;
        }

        public void Cancel(MessageTask task)
        {
            _cancelledTasks.Add(task.name);
        }

        public void Resume(MessageTask task)
        {
            _cancelledTasks.Remove(task.name);
        }

        public bool IsCancelled(MessageTask task)
        {
            return _cancelledTasks.Contains(task.name);
        }

        public MessageTask FindByName(string n)
        {
            return messageSystem.tasks.FirstOrDefault(task => task.name.Equals(n));
        }

        public void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.D))
            {
                debugUI.SetActive(true);
            }
        }
    }
}