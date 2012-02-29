using Microsoft.Xna.Framework;
using MiUtil;
using System.Collections.Generic;
using System;

namespace Micycle
{
    public class MicycleGameSystem : MiComponent
    {
        public static Random rnd = new Random();

        private int cityPeople;
        private int schoolStudents;
        private int schoolTeachers;
        private int researchers;
        private int factoryWorkers;
        private int cityBums;

        private int factoryWorkerCapacity;
        private int researcherCapacity;
        private int schoolCapacity;

        private float educationBudget;
        private float factoryWorkerWage;
        private float researcherWage;

        private int time;

        private float cityMoney;
        private float ownerMoney;
        private float researchPoints;
        

        private bool sendMouseFromCityToSchool;
        public bool SendMouseFromCityToSchool { get { return sendMouseFromCityToSchool; } }
        private bool sendMouseFromCityToFactory;
        public bool SendMouseFromCityToFactory { get { return sendMouseFromCityToFactory; } }
        private bool sendMouseFromCityToRnd;
        public bool SendMouseFromCityToRnd { get { return sendMouseFromCityToRnd; } }

        private bool sendMouseFromSchoolToCity;
        public bool SendMouseFromSchoolToCity { get { return sendMouseFromSchoolToCity; } }
        private bool sendMouseFromSchoolToFactory;
        public bool SendMouseFromSchoolToFactory { get { return sendMouseFromSchoolToFactory; } }
        private bool sendMouseFromSchoolToRnd;
        public bool SendMouseFromSchoolToRnd { get { return sendMouseFromSchoolToRnd; } }

        private bool sendMouseFromRndToCity;
        public bool SendMouseFromRndToCity { get { return sendMouseFromRndToCity; } }
        private bool sendRobotFromRndToFactory;
        public bool SendRobotFromRndToFactory { get { return sendRobotFromRndToFactory; } }

        private bool sendMouseFromFactoryToCity;
        public bool SendMouseFromFactoryToCity { get { return sendMouseFromFactoryToCity; } }

        float birthRate, deathRate;
        float factoryRetirementRate;
        float rndRetirementRate;

        float researchRate;

        float bumToRndRate;
        float bumToFactoryRate;

        float cityPeopleBirthBias;
        float studentsBirthBias;
        float researchersBirthBias;
        float factoryWorkersBirthBias;
        float bumsBirthBias;

        int costOfLiving;
        int year;
        int month;
        int schoolSendRate;
        int studyTime;
        int INCOME_PER_WORKER = 30;

        int educationLevel;
        int max_educationLevel;

        private List<StudentWrapper> students;

        public MicycleGameSystem(Micycle game)
            : base(game)
        {

            birthRate = 0.2f;
            deathRate = 0.1f;
            researchRate = 0.5f;
            year = 1200;
            month = year / 12;
            schoolSendRate = month/5;
            studyTime = 200;
            schoolCapacity = 5;
            researcherCapacity = 45;
            factoryWorkerCapacity = 45;

            bumToFactoryRate = 0.02f;
            bumToRndRate = 0.02f;

            factoryRetirementRate = 0.2f;
            rndRetirementRate = 0.2f;

            cityPeople = 400;


            cityPeopleBirthBias = 0f;
            studentsBirthBias = 0.1f;
            researchersBirthBias = 0.2f;
            factoryWorkersBirthBias = 0.2f;
            bumsBirthBias = 0.5f;

            factoryWorkerWage = 10;
            researcherWage = 10;
            educationLevel = 90;
            max_educationLevel = 100;

            costOfLiving = 10;

            students = new List<StudentWrapper>();
        }

        private void AddEducationBudget(int dx) 
        {
            educationBudget += dx;
            if (educationBudget < 0) educationBudget = 0;
        }

        private void AddTeachers(int dx) 
        {
            schoolTeachers += dx;
            if (schoolTeachers < 0) schoolTeachers = 0;
        }

        private void AddWorkerWage(int dx)
        {
            factoryWorkerWage += dx;
            if (factoryWorkerWage < 0) factoryWorkerWage = 0;
        }

        private void AddWorkerCapacity(int dx)
        {
            factoryWorkerCapacity += dx;
            if (factoryWorkerCapacity < 0) factoryWorkerCapacity = 0;
        }

        private void AddResearcherWage(int dx)
        {
            researcherWage += dx;
            if (researcherWage < 0) researcherWage = 0;
        }

        private void AddResearcherCapacity(int dx)
        {
            researcherCapacity += dx;
            if (researcherCapacity < 0) researcherCapacity = 0;
        }

        
        
        public void updateCity()
        {
            //update city population
            //update the bums
            if (time % year == 0)
            {
                cityMoney -= (cityBums + cityPeople) * costOfLiving;
                cityPeople += (int)Math.Ceiling(((birthRate) * 
                    (cityPeople*cityPeopleBirthBias + factoryWorkers*factoryWorkersBirthBias + 
                    researchers*researchersBirthBias + students.Count*studentsBirthBias + cityBums*bumsBirthBias)));

                cityBums -= (int)Math.Ceiling((deathRate) * cityBums);
            }
        }

        public void updateFactory()
        {
            sendMouseFromFactoryToCity = false;

            if (time % month == 0)
            {
                ownerMoney += INCOME_PER_WORKER * factoryWorkers;
                cityMoney += factoryWorkerWage * factoryWorkers;
            }
            if (time % year == 0)
            {
                int toRetire = (int)Math.Ceiling(factoryRetirementRate * factoryWorkers);
                if (toRetire > 0)
                {
                    sendMouseFromFactoryToCity = true;
                    cityBums += toRetire;
                    factoryWorkers -= toRetire;
                }
            }
        }

        public void updateSchool()
        {
            if( time%year == 0 )
                ownerMoney -= educationBudget;

            List<StudentWrapper> toRemove = new List<StudentWrapper>();

            sendMouseFromSchoolToCity = false;
            sendMouseFromSchoolToFactory = false;
            sendMouseFromSchoolToRnd = false;

            foreach (StudentWrapper st in students)
            {
                st.timeLeft--;
                if (st.timeLeft <= 0)
                {
                    toRemove.Add(st);
                }
            }

            foreach (StudentWrapper st in toRemove)
            {
                students.Remove(st);
                graduateStudent();
            }

            
        }

        public void graduateStudent()
        {
            //do whatever needed when student leaves school
            double num = rnd.NextDouble();

            double S = (float)educationLevel / max_educationLevel;

            double researcherPull = (S * (researcherWage + educationLevel)) / (researcherWage + factoryWorkerWage + educationLevel);
            double factoryWorkerPull = (S * (factoryWorkerWage)) / (researcherWage + factoryWorkerWage + educationLevel);

            if (num >= 0 && num <= researcherPull && researcherCapacity > researchers)
            {
                //send to researchCenter
                sendMouseFromSchoolToRnd = true;
                researchers++;
                return;
            }

            else if (num > researcherPull && num <= researcherPull + factoryWorkerPull && factoryWorkerCapacity > factoryWorkers)
            {
                //send to factory
                sendMouseFromSchoolToFactory = true;
                factoryWorkers++;
                return;
            }


            //send to bum
            sendMouseFromSchoolToCity = true;
            cityBums++;
        }

        public void updateResearchCenter()
        {
            sendMouseFromRndToCity = false;
            if (time % month == 0)
            {
                ownerMoney -= researcherWage * researchers;
                cityMoney += researcherWage * researchers;
                researchPoints += researchRate * researchers;
            }
            if (time % year == 0)
            {
                int toRetire = (int)Math.Ceiling(rndRetirementRate * researchers);
                if (toRetire > 0)
                {
                    sendMouseFromRndToCity = true;
                    cityBums += toRetire;
                    researchers -= toRetire;
                }
            }
        }


        public override void Update(GameTime gameTime)
        {
            System.Console.WriteLine(cityPeople + " " + cityBums + " st=" + students.Count + " r=" + researchers + " f=" + factoryWorkers);
            time++;
            updateCity();
            updateFactory();
            updateSchool();
            updateResearchCenter();
            if (cityPeople > 0 && time % schoolSendRate == 0 && students.Count < schoolCapacity )
            {
                cityPeople--;
                sendMouseFromCityToSchool = true;
                students.Add(new StudentWrapper(studyTime));
            }
            else
            {
                sendMouseFromCityToSchool = false;
            }

            double num = rnd.NextDouble();
            if (cityBums > 0 && researcherCapacity > researchers && num <= bumToRndRate)
            {
                sendMouseFromCityToRnd = true;
                cityBums--;
                researchers++;
            }
            else
            {
                sendMouseFromCityToRnd = false;
            }

            num = rnd.NextDouble();
            if (cityBums > 0 && factoryWorkerCapacity > factoryWorkers && num <= bumToFactoryRate)
            {
                sendMouseFromCityToFactory = true;
                cityBums--;
                factoryWorkers++;
            }
            else
            {
                sendMouseFromCityToFactory = false;
            }
            if (time % year == 0) time = 0;
        }
    }

    class StudentWrapper
    {
        public int timeLeft;

        public StudentWrapper(int t) { timeLeft = t; }
    }
}
