using Microsoft.Xna.Framework;
using MiUtil;
using System.Collections.Generic;
using System;

namespace Micycle
{
    class MicycleGameSystem : MiComponent
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
        private int schoolFacultyCapacity;


        private float educationBudget;
        private float factoryWorkerWage;
        private float researcherWage;

        private int time;

        private float cityMoney;
        private float ownerMoney;
        private float researchPoints;

        private float birthRate, deathRate;
        private float factoryRetirementRate;
        private float rndRetirementRate;

        private float researchRate;

        private float bumToRndRate;
        private float bumToFactoryRate;

        private float cityPeopleBirthBias;
        private float studentsBirthBias;
        private float researchersBirthBias;
        private float factoryWorkersBirthBias;
        private float bumsBirthBias;

        private int costOfLiving;
        private int year;
        private int month;
        private int schoolSendRate;
        private int studyTime;
        private int INCOME_PER_WORKER = 30;

        private int educationLevel;
        private int max_educationLevel;

        private List<StudentWrapper> students;

        public MiSemaphoreSet CityToSchool;
        public MiSemaphoreSet CityToFactory;
        public MiSemaphoreSet CityToRnd;
        public MiSemaphoreSet SchoolToCity;
        public MiSemaphoreSet SchoolToFactory;
        public MiSemaphoreSet SchoolToRnd;
        public MiSemaphoreSet RndToCity;
        public MiSemaphoreSet RndToSchool;
        public MiSemaphoreSet RndToFactory;
        public MiSemaphoreSet FactoryToCity;

        public void Signal(ref int sema)
        {
            sema++;
        }

        public bool Wait(ref int sema)
        {
            if (sema > 0)
            {
                sema--;
                return true;
            }
            return false;
        }

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
            educationBudget = 10;
            educationLevel = 90;
            max_educationLevel = 100;

            costOfLiving = 10;

            students = new List<StudentWrapper>();

            CityToSchool = new MiSemaphoreSet();
            CityToFactory = new MiSemaphoreSet();
            CityToRnd = new MiSemaphoreSet();
            SchoolToCity = new MiSemaphoreSet();
            SchoolToFactory = new MiSemaphoreSet();
            SchoolToRnd = new MiSemaphoreSet();
            RndToCity = new MiSemaphoreSet();
            RndToSchool = new MiSemaphoreSet();
            RndToFactory = new MiSemaphoreSet();
            FactoryToCity = new MiSemaphoreSet();
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

        public void SchoolUpButtonAction()
        {
            educationBudget += 10;
        }

        public void SchoolDownButtonAction()
        {
            educationBudget -= 10;
        }

        public void SchoolLeftButtonAction()
        {
            if (researchers < researcherCapacity && schoolTeachers > 0)
            {
                researchers++;
                schoolTeachers--;
                Signal(ref SchoolToRnd.SendFromAToB);
            }
            
        }

        public void SchoolRightButtonAction()
        {
            schoolTeachers++;
            researchers--;
            Signal(ref RndToSchool.SendFromAToB);
        }

        public void FactoryUpButtonAction()
        {
            factoryWorkerWage++;
        }

        public void FactoryDownButtonAction()
        {
            factoryWorkerWage--;
        }

        public void FactoryLeftButtonAction()
        {
            factoryWorkerCapacity--;
        }

        public void FactoryRightButtonAction()
        {
            factoryWorkerCapacity++;
        }

        public void RndUpButtonAction()
        {
            researcherWage++;
        }

        public void RndDownButtonAction()
        {
            researcherWage--;
        }

        public void RndLeftButtonAction()
        {
            researcherCapacity--;
        }

        public void RndRightButtonAction()
        {
            researcherCapacity++;
        }

        public string printStats()
        {
            //TO-DO: Return a string containing all the necessay game information for the player
            return "";
        }
        
        private void updateCity()
        {
            //update city population
            //update the bums
            if (time % year == 0)
            {
                cityMoney -= (cityBums + cityPeople) * costOfLiving;
                cityPeople += (int)Math.Ceiling(((birthRate) * 
                    (cityPeople*cityPeopleBirthBias + factoryWorkers*factoryWorkersBirthBias + 
                    (researchers+schoolTeachers)*researchersBirthBias + students.Count*studentsBirthBias + cityBums*bumsBirthBias)));

                cityBums -= (int)Math.Ceiling((deathRate) * cityBums);
            }
        }

        private void updateFactory()
        {
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
                    Signal(ref FactoryToCity.SendFromAToB);
                    cityBums += toRetire;
                    factoryWorkers -= toRetire;
                }
            }
        }

        private void updateSchool()
        {
            if( time%year == 0 )
                ownerMoney -= educationBudget;

            List<StudentWrapper> toRemove = new List<StudentWrapper>();

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

        private void graduateStudent()
        {
            //do whatever needed when student leaves school
            double num = rnd.NextDouble();

            double S = (float)educationLevel / max_educationLevel;

            double researcherPull = (S * (researcherWage + educationLevel)) / 
                                (researcherWage + factoryWorkerWage + educationLevel);
            double factoryWorkerPull = (S * (factoryWorkerWage)) / 
                                (researcherWage + factoryWorkerWage + educationLevel);

            if (num >= 0 && num <= researcherPull && researcherCapacity > researchers)
            {
                //send to researchCenter
                Signal(ref SchoolToRnd.SendFromAToB);
                researchers++;
                return;
            }

            else if (num > researcherPull && num <= researcherPull + factoryWorkerPull && factoryWorkerCapacity > factoryWorkers)
            {
                //send to factory
                Signal(ref SchoolToFactory.SendFromAToB);
                factoryWorkers++;
                return;
            }


            //send to bum
            Signal(ref SchoolToCity.SendFromAToB);
            cityBums++;
        }

        private void updateResearchCenter()
        {
            if (time % month == 0)
            {
                ownerMoney -= researcherWage * (schoolTeachers+researchers);
                cityMoney += researcherWage * (schoolTeachers+researchers);
                researchPoints += researchRate * researchers;
            }
            if (time % year == 0)
            {
                int toRetire = (int)Math.Ceiling(rndRetirementRate * researchers);
                if (toRetire > 0)
                {
                    Signal(ref RndToCity.SendFromAToB);
                    cityBums += toRetire;
                    researchers -= toRetire;
                }
            }
        }


        public override void Update(GameTime gameTime)
        {
            System.Console.WriteLine(cityPeople + " " + cityBums + " st=" + students.Count +" teacher=" + schoolTeachers+ "r=" + researchers + " f=" + factoryWorkers);
            time++;
            updateCity();
            updateFactory();
            updateSchool();
            updateResearchCenter();
            if (cityPeople > 0 && time % schoolSendRate == 0 && students.Count < schoolCapacity )
            {
                cityPeople--;
                Signal(ref CityToSchool.SendFromAToB);
                students.Add(new StudentWrapper(studyTime));
            }

            double num = rnd.NextDouble();
            if (cityBums > 0 && researcherCapacity > researchers && num <= bumToRndRate)
            {
                Signal(ref CityToRnd.SendFromAToB);
                cityBums--;
                researchers++;
            }

            num = rnd.NextDouble();
            if (cityBums > 0 && factoryWorkerCapacity > factoryWorkers && num <= bumToFactoryRate)
            {
                Signal(ref CityToFactory.SendFromAToB);
                cityBums--;
                factoryWorkers++;
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
