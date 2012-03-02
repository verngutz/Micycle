﻿using Microsoft.Xna.Framework;
using MiUtil;
using System.Collections.Generic;
using System;

namespace Micycle
{
    class MicycleGameSystem : MiComponent
    {
        public static Random rnd = new Random();

        private int cityPeople;
        private int schoolTeachers;
        private int researchers;
        private int factoryWorkers;
        private int cityBums;

        private int factoryWorkerCapacity;
        private int schoolCapacity;
      


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
        private int factoryUpkeep;
        private int rndUpkeep;

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
            birthRate = 1.5f;
            deathRate = 0.1f;
            researchRate = 0.5f;
            year = 1200;
            month = year / 12;
            schoolSendRate = month/5;
            studyTime = 200;
            schoolCapacity = 25;
            //researcherCapacity = 5;
            factoryWorkerCapacity = 10;

            bumToFactoryRate = 0.02f;
            bumToRndRate = 0.02f;

            factoryRetirementRate = 0.02f;
            rndRetirementRate = 0.02f;

            cityPeople = 400;

            cityPeopleBirthBias = 0f;
            studentsBirthBias = 0.1f;
            researchersBirthBias = 0.2f;
            factoryWorkersBirthBias = 0.2f;
            bumsBirthBias = 0.5f;

            factoryWorkerWage = 100;
            researcherWage = 10;
            educationBudget = 100;
            educationLevel = 0;
            max_educationLevel = 100;

            costOfLiving = 10;
            factoryUpkeep = factoryWorkerCapacity;
            rndUpkeep = 0;

            students = new List<StudentWrapper>();
            schoolTeachers = 1;


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
            factoryUpkeep = factoryWorkerCapacity;
        }

        private void AddResearcherWage(int dx)
        {
            researcherWage += dx;
            if (researcherWage < 0) researcherWage = 0;
        }

        private void AddResearcherCapacity(int dx)
        {
            
        }

        public void SchoolUpButtonAction()
        {
            AddEducationBudget(10);
        }

        public void SchoolDownButtonAction()
        {
            AddEducationBudget(-10);
        }

        public void SchoolLeftButtonAction()
        {
            if (schoolTeachers > 0)
            {
                researchers++;
                schoolTeachers--;
                Signal(ref SchoolToRnd.SendFromAToB);
            }
            
        }

        public void SchoolRightButtonAction()
        {
            if (researchers > 0)
            {
                schoolTeachers++;
                researchers--;
                 Signal(ref RndToSchool.SendFromAToB);
            }

        }

        public void FactoryUpButtonAction()
        {
            AddWorkerWage(1);
        }

        public void FactoryDownButtonAction()
        {
            AddWorkerWage(-1);
        }

        public void FactoryLeftButtonAction()
        {
            AddWorkerCapacity(-1);
            
            if (factoryWorkerCapacity > factoryWorkers)
            {
                factoryWorkers--;
                cityBums++;
                Signal(ref FactoryToCity.SendFromAToB);

            }
        }

        public void FactoryRightButtonAction()
        {
            AddWorkerCapacity(1);
            
        }

        public void RndUpButtonAction()
        {
            AddResearcherWage(1);
        }

        public void RndDownButtonAction()
        {
            AddResearcherWage(-1);
        }

        public void RndLeftButtonAction()
        {
            
        }

        public void RndRightButtonAction()
        {
            //AddResearcherCapacity(1);
        }

        public string printStats()
        {
            //TO-DO: Return a string containing all the necessay game information for the player
            return String.Format("$: " + ownerMoney + " city$: " + cityMoney + "\nkids: " + cityPeople + " bums: "+ cityBums+ "\nstudents: " + students.Count + "/" + schoolCapacity + " teachers: " + schoolTeachers + " education budget: " + educationBudget + "\nfactory " + factoryWorkers + "/" + factoryWorkerCapacity + " factory wage: " + factoryWorkerWage + "\nrnd: " + researchers +  " rnd budget: " + researcherWage);
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
                ownerMoney += INCOME_PER_WORKER * factoryWorkers + factoryUpkeep;
                cityMoney += factoryWorkerWage * factoryWorkers ;
           
                int toRetire = (int)Math.Round(factoryRetirementRate * factoryWorkers);
                if (toRetire > 0)
                {

                    for( int i = 0; i < toRetire; i++ )
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
            educationLevel = (int)(educationBudget * schoolTeachers / students.Count);
            if (schoolTeachers > students.Count)
                educationLevel = (int)educationBudget;
            double S = (float)(educationLevel) / max_educationLevel;

            double researcherPull = (S * (researcherWage + educationLevel)) / 
                                (researcherWage + factoryWorkerWage + educationLevel);
            double factoryWorkerPull = (S * (factoryWorkerWage)) / 
                                (researcherWage + factoryWorkerWage + educationLevel);

            if (researcherWage == 0 && factoryWorkerWage != 0)
            {
                researcherPull = 0;
                factoryWorkerPull = S;
            }
            else if (researcherWage != 0 && factoryWorkerWage == 0)
            {
                researcherPull = S;
                factoryWorkerPull = 0;
            }
            else if( researcherWage == 0 && factoryWorkerWage == 0 )
            {
                researcherPull = 0;
                factoryWorkerPull = 0;
            }
            else if (factoryWorkers == factoryWorkerCapacity)
            {
                factoryWorkerPull = 0;
                researcherPull = S;
            }
            if (num >= 0 && num <= researcherPull )
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
                ownerMoney -= researcherWage * (schoolTeachers+researchers) + rndUpkeep;
                cityMoney += researcherWage * (schoolTeachers+researchers);
                researchPoints += researchRate * researchers;
            
                int toRetire = (int)Math.Round(rndRetirementRate * researchers);
                if (toRetire > 0)
                {

                    for (int i = 0; i < toRetire; i++)
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
            if (time%year == 0 && cityBums>0 && num <= bumToRndRate)
            {
                educationLevel = (int)(educationBudget * schoolTeachers / students.Count);
                if (schoolTeachers > students.Count)
                    educationLevel = (int)educationBudget;
                double S = (float)bumToRndRate;

                double researcherPull = (S * (researcherWage + educationLevel)) /
                                    (researcherWage + factoryWorkerWage + educationLevel);
                double factoryWorkerPull = (S * (factoryWorkerWage)) /
                                    (researcherWage + factoryWorkerWage + educationLevel);

                
                if (researcherWage == 0 && factoryWorkerWage != 0)
                {
                    factoryWorkerPull = S;
                }
                else if (researcherWage != 0 && factoryWorkerWage == 0)
                {
                    researcherPull = S;
                }
                else if (researcherWage == 0 && factoryWorkerWage == 0)
                {
                    researcherPull = 0;
                    factoryWorkerPull = 0;
                }
                else if (factoryWorkers == factoryWorkerCapacity)
                {
                    factoryWorkerPull = 0;
                    researcherPull = S;
                }

                if (num >= 0 && num <= researcherPull)
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
            }

            if (time%year == 0 && cityBums > 0 && num <= bumToRndRate)
            {
                Signal(ref CityToRnd.SendFromAToB);
                cityBums--;
                researchers++;
            }

            num = rnd.NextDouble();
            if (time%year == 0 && cityBums > 0 && factoryWorkerCapacity > factoryWorkers && num <= bumToFactoryRate)
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
