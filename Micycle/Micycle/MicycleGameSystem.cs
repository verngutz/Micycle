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

        public int SendMouseFromCityToSchool = 0;
        public int MouseHasReachedSchoolFromCity = 0;
        public int SendMouseFromCityToFactory = 0;
        public int MouseHasReachedFactoryFromCity = 0;
        public int SendMouseFromCityToRnd = 0;
        public int MouseHasReachedRndFromCity = 0;
        public int SendMouseFromSchoolToCity = 0;
        public int MouseHasReachedCityFromSchool = 0;
        public int SendMouseFromSchoolToFactory = 0;
        public int MouseHasReachedFactoryFromSchool = 0;
        public int SendMouseFromSchoolToRnd = 0;
        public int MouseHasReachedRndFromSchool = 0;
        public int SendMouseFromRndToCity = 0;
        public int MouseHasReachedCityFromRnd = 0;
        public int SendMouseFromRndToSchool = 0;
        public int MouseHasReachedSchoolFromRnd = 0;
        public int SendRobotFromRndToFactory = 0;
        public int RobotHasReachedFactoryFromRnd = 0;
        public int SendMouseFromFactoryToCity = 0;
        public int MouseHasReachedCityFromFactory = 0;

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

        public void SchoolUpButtonAction()
        {
            //TO-DO: Add action code here.
        }

        public void SchoolDownButtonAction()
        {
            //TO-DO: Add action code here.
        }

        public void SchoolLeftButtonAction()
        {
            //TO-DO: Add action code here.
        }

        public void SchoolRightButtonAction()
        {
            //TO-DO: Add action code here.
        }

        public void FactoryUpButtonAction()
        {
            //TO-DO: Add action code here.
        }

        public void FactoryDownButtonAction()
        {
            //TO-DO: Add action code here.
        }

        public void FactoryLeftButtonAction()
        {
            //TO-DO: Add action code here.
        }

        public void FactoryRightButtonAction()
        {
            //TO-DO: Add action code here.
        }

        public void RndUpButtonAction()
        {
            //TO-DO: Add action code here.
        }

        public void RndDownButtonAction()
        {
            //TO-DO: Add action code here.
        }

        public void RndLeftButtonAction()
        {
            //TO-DO: Add action code here.
        }

        public void RndRightButtonAction()
        {
            //TO-DO: Add action code here.
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
                    Signal(ref SendMouseFromFactoryToCity);
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
                Signal(ref SendMouseFromSchoolToRnd);
                researchers++;
                return;
            }

            else if (num > researcherPull && num <= researcherPull + factoryWorkerPull && factoryWorkerCapacity > factoryWorkers)
            {
                //send to factory
                Signal(ref SendMouseFromSchoolToFactory);
                factoryWorkers++;
                return;
            }


            //send to bum
            Signal(ref SendMouseFromSchoolToCity);
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
                    Signal(ref SendMouseFromRndToCity);
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
                Signal(ref SendMouseFromCityToSchool);
                students.Add(new StudentWrapper(studyTime));
            }

            double num = rnd.NextDouble();
            if (cityBums > 0 && researcherCapacity > researchers && num <= bumToRndRate)
            {
                Signal(ref SendMouseFromCityToRnd);
                cityBums--;
                researchers++;
            }

            num = rnd.NextDouble();
            if (cityBums > 0 && factoryWorkerCapacity > factoryWorkers && num <= bumToFactoryRate)
            {
                Signal(ref SendMouseFromCityToFactory);
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
