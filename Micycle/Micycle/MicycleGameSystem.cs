using Microsoft.Xna.Framework;
using MiUtil;
using System.Collections.Generic;
using System;

namespace Micycle
{
    class MicycleGameSystem : MiComponent
    {
        public static Random rnd = new Random();

        private float cityMoney;
        private int cityPeople;
        private int cityBums;
        private float birthRate, deathRate;
        private float bumToWorkRate;
        private int costOfLiving;
        private float numKidsSendRate;
        private int schoolSendRate;

        private float ownerMoney;
        private int time;
        private int yearCtr;
        private int year;
        private int month;

        private int schoolTeachers;
        private int schoolCapacity;
        private float educationBudget;
        private int studyTime;
        private int educationLevel;
        private int max_educationLevel;
        private List<StudentWrapper> students;

        private int researchers;
        private float researcherWage;
        private float researchPoints;
        private float rndRetirementRate;
        private float researchRate;
        private int rndUpkeep;
        private int robots;
        private int robotEfficiency;
        private int robotCost;

        private int factoryWorkers;
        private int factoryWorkerCapacity;
        private float factoryWorkerWage;
        private float factoryRetirementRate;
        private int factoryUpkeep;
        private int REVENUE_PER_WORKER;
        private int factoryDoorWait;
        private int factoryDoorWaitLimit;

        private float cityPeopleBirthBias;
        private float studentsBirthBias;
        private float researchersBirthBias;
        private float factoryWorkersBirthBias;
        private float bumsBirthBias;

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

        public void init()
        {
            time = 0;
            year = 2400;
            yearCtr = 0;
            month = year / 12;
            ownerMoney = 3000;

            //city stats
            cityPeople = 110;
            cityBums = 0;
            cityMoney = 3000;
            costOfLiving = 25;
            birthRate = 1.5f;
            deathRate = 0.1f;
            numKidsSendRate = 0.02f;
            schoolSendRate = month / 5;
            bumToWorkRate = 0.02f;

            //students stats
            students = new List<StudentWrapper>();
            schoolTeachers = 1;
            studyTime = 200;
            schoolCapacity = 25;
            educationBudget = 100;
            educationLevel = 0;
            max_educationLevel = 100;

            //research center stats
            researchPoints = 0;
            researchers = 0;
            researchRate = 0.5f;
            rndRetirementRate = 0.02f;
            researcherWage = 10;
            rndUpkeep = 0;
            robots = 0 ;
            robotEfficiency = 5;
            robotCost = 200;

            //factory stats
            factoryWorkers = 0;
            factoryWorkerCapacity = 10;
            factoryRetirementRate = 0.02f;
            factoryWorkerWage = 25;
            REVENUE_PER_WORKER = 40;
            factoryUpkeep = factoryWorkerCapacity;
            factoryDoorWait = 0;
            factoryDoorWaitLimit = 30;

            //birth control
            cityPeopleBirthBias = 0f;
            studentsBirthBias = 0.1f;
            researchersBirthBias = 0.2f;
            factoryWorkersBirthBias = 0.2f;
            bumsBirthBias = 0.5f;

            //semaphores
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
        public MicycleGameSystem(Micycle game)
            : base(game)
        {

            init();
        }

        public void Reset()
        {
            init();
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
            float origWage = factoryWorkerWage;
            factoryWorkerWage += dx;
            if (factoryWorkerWage < 0) factoryWorkerWage = 0;

            if (origWage > factoryWorkerWage)
            {
                int toRetire = (int)Math.Round(factoryWorkers * (origWage - factoryWorkerWage) / origWage);
                
                for (int i = 0; i < toRetire; i++)
                {
                    Signal(ref FactoryToCity.SendFromAToB);
                }
                factoryWorkers -= toRetire;
            }
        }

        public void AddRobots(int dx)
        {
            if (dx > 0)
            {
                for (int i = 0; i < dx; i++)
                    if ((robots + RndToFactory.SendFromAToB + RndToFactory.HasReachedWaitQueueTail + 1) * robotEfficiency < factoryWorkerCapacity)
                    {
                        Signal(ref RndToFactory.SendFromAToB);
                    }
            }
            else
            {

            }
        }

        private void AddWorkerCapacity(int dx)
        {
            factoryWorkerCapacity += dx;
            if (factoryWorkerCapacity < 0) factoryWorkerCapacity = 0;
            factoryUpkeep = factoryWorkerCapacity;
        }

        private void AddResearcherWage(int dx)
        {
            float origWage = researcherWage;
            researcherWage += dx;
            if (researcherWage < 0) researcherWage = 0;

            if (origWage > researcherWage)
            {
                int toRetire = (int)Math.Round((researchers)*(origWage-researcherWage)/origWage );
                
                for (int i = 0; i < toRetire; i++)
                {
                   
                    Signal(ref RndToCity.SendFromAToB);
                    
                }
                researchers -= toRetire;

                toRetire = (int)Math.Round((schoolTeachers) * (origWage - researcherWage) / origWage);
                for (int i = 0; i < toRetire; i++)
                {

                    Signal(ref SchoolToCity.SendFromAToB);

                }
                schoolTeachers -= toRetire;
            }
        }


        private void AddResearcherCapacity(int dx)
        {
            
        }

        public void SchoolUpButtonAction()
        {

            AddEducationBudget(10);
            schoolCapacity = (int)(educationBudget / 4);
        }

        public void SchoolDownButtonAction()
        {
            AddEducationBudget(-10);
            schoolCapacity = (int)(educationBudget / 4);
        }

        public void SchoolLeftButtonAction()
        {
            if (schoolTeachers > 0)
            {
                schoolTeachers--;
                Signal(ref SchoolToRnd.SendFromAToB);
            }
            
        }

        public void SchoolRightButtonAction()
        {
            if (researchers > 0)
            {
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
            
            if (factoryWorkerCapacity < factoryWorkers)
            {
                factoryWorkers--;
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
            if (researchPoints > robotCost)
            {
                researchPoints -= robotCost;
                AddRobots(1);
            }
        }

        public string printStats()
        {
            //TO-DO: Return a string containing all the necessay game information for the player
            return String.Format("year: " + yearCtr +" $: " + ownerMoney + " city$: " + cityMoney + "\nkids: " + cityPeople + " bums: "+ cityBums+ "\nstudents: " + students.Count + "/" + schoolCapacity + " teachers: " + schoolTeachers + " education budget: " + educationBudget + 
                "\nfactory: " + factoryWorkers + "("+robots+")/" + factoryWorkerCapacity + " factory wage: " + factoryWorkerWage + 
                "\nrnd: " + researchers +  " rnd budget: " + researcherWage + " tech points: " + researchPoints);
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
            if (factoryWorkerWage == 0 && factoryWorkers > 0)
            {
                for (int i = 0; i < factoryWorkers; i++)
                {
                    Signal(ref FactoryToCity.SendFromAToB);
                    factoryWorkers = 0;
                }
            }

            if (time % month == 0)
            {
                ownerMoney += (REVENUE_PER_WORKER - factoryWorkerWage) * factoryWorkers  + robots*REVENUE_PER_WORKER;
                ownerMoney -= factoryUpkeep;
                cityMoney += factoryWorkerWage * factoryWorkers ;
           
                int toRetire = (int)Math.Ceiling(factoryRetirementRate * factoryWorkers);
                if (toRetire > 0)
                {

                    for( int i = 0; i < toRetire; i++ )
                        Signal(ref FactoryToCity.SendFromAToB);

                    
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

            double researcherPull = (S * (researcherWage + educationLevel/25 )) / 
                                (researcherWage + factoryWorkerWage + educationLevel/25 );
            double factoryWorkerPull = (S * (factoryWorkerWage)) / 
                                (researcherWage + factoryWorkerWage + educationLevel/25);

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
            
            if (num >= 0 && num <= researcherPull )
            {
                //send to researchCenter
                Signal(ref SchoolToRnd.SendFromAToB);
                return;
            }

            else if (num > researcherPull && num <= researcherPull + factoryWorkerPull && factoryWorkerCapacity > factoryWorkers)
            {
                //send to factory
                Signal(ref SchoolToFactory.SendFromAToB);
                return;
            }

            //send to bum
            Signal(ref SchoolToCity.SendFromAToB);
        }

        private void updateResearchCenter()
        {
            if (researcherWage == 0 && (schoolTeachers + researchers) > 0)
            {
                for (int i = 0; i < schoolTeachers; i++)
                {
                    Signal(ref SchoolToCity.SendFromAToB);
                }
                for (int i = 0; i < researchers; i++)
                {
                    Signal(ref RndToCity.SendFromAToB);
                }

                schoolTeachers = 0;
                researchers = 0;
            }
            if (time % (month*2) == 0)
            {
                ownerMoney -= researcherWage * (schoolTeachers+researchers) + rndUpkeep;
                cityMoney += researcherWage * (schoolTeachers+researchers);
                researchPoints += researchRate * researchers;
            
                int toRetire = (int)Math.Ceiling(rndRetirementRate * researchers);
                if (toRetire > 0)
                {

                    for (int i = 0; i < toRetire; i++)
                        Signal(ref RndToCity.SendFromAToB);

                    researchers -= toRetire;
                }

            }
        }


        public override void Update(GameTime gameTime)
        {
            if (Wait(ref SchoolToCity.HasReachedWaitQueueTail))
            {
                Signal(ref SchoolToCity.Accept);
                cityBums++;
            }

            if ((factoryWorkers + robots * robotEfficiency) < factoryWorkerCapacity && Wait(ref SchoolToFactory.HasReachedWaitQueueTail))
            {
                Signal(ref SchoolToFactory.Accept);
                factoryWorkers++;    
            }
            else if (Wait(ref SchoolToFactory.HasReachedWaitQueueTail))
            {
                factoryDoorWait++;
                if (factoryDoorWait == factoryDoorWaitLimit)
                {
                    Signal(ref SchoolToFactory.Reject);
                }
                else
                {
                    Signal(ref SchoolToFactory.HasReachedWaitQueueTail);
                }
            }
            if (Wait(ref SchoolToRnd.HasReachedWaitQueueTail))
            {
                Signal(ref SchoolToRnd.Accept);
                researchers++;
            }

            if (students.Count < schoolCapacity && Wait(ref CityToSchool.HasReachedWaitQueueTail))
            {
                Signal(ref CityToSchool.Accept);
                students.Add(new StudentWrapper(studyTime));
            }

            if ((factoryWorkers+robots*robotEfficiency) < factoryWorkerCapacity &&  Wait(ref CityToFactory.HasReachedWaitQueueTail))
            {
                Signal(ref CityToFactory.Accept);
                factoryWorkers++;  
            }

            if (Wait(ref CityToRnd.HasReachedWaitQueueTail))
            {
                Signal( ref CityToRnd.Accept );
                researchers++;
            }

            if (Wait(ref RndToCity.HasReachedWaitQueueTail))
            {
                Signal(ref RndToCity.Accept);
                cityBums++;
            }

            if (Wait(ref RndToSchool.HasReachedWaitQueueTail))
            {
                Signal(ref RndToSchool.Accept);
                schoolTeachers++;
            }

            if (Wait(ref FactoryToCity.HasReachedWaitQueueTail))
            {
                Signal(ref FactoryToCity.Accept);
                cityBums++;
            }
            if (Wait(ref RndToFactory.HasReachedWaitQueueTail))
            {
                Signal(ref RndToFactory.Accept);
                robots++;
                int delta = (robots * robotEfficiency) + factoryWorkers - factoryWorkerCapacity;

                if (delta > 0)
                {
                    for (int i = 0; i < delta; i++)
                        Signal(ref FactoryToCity.SendFromAToB);
                    factoryWorkers -= delta;
                    if (factoryWorkers < 0) factoryWorkers = 0;
                }
            }
            time++;
            updateCity();
            updateFactory();
            updateSchool();
            updateResearchCenter();
            if (cityPeople > 0 && time % schoolSendRate == 0 && students.Count < schoolCapacity )
            {
                int toSend = (int)Math.Ceiling(cityPeople*numKidsSendRate);
                toSend = Math.Min(toSend, schoolCapacity - students.Count);

                for (int i = 0; i < toSend; i++)
                {
                    Signal(ref CityToSchool.SendFromAToB);
                    
                }
                cityPeople -= toSend;
            }

            double num = rnd.NextDouble();
            if (time%year == 0 && cityBums>0 && num <= bumToWorkRate)
            {
                educationLevel = (int)(educationBudget * schoolTeachers / students.Count);
                if (schoolTeachers > students.Count)
                    educationLevel = (int)educationBudget;
                double S = (float)bumToWorkRate;

                double researcherPull = (S * (researcherWage + educationLevel/10)) /
                                    (researcherWage + factoryWorkerWage + educationLevel/10);
                double factoryWorkerPull = (S * (factoryWorkerWage)) /
                                    (researcherWage + factoryWorkerWage + educationLevel/10);

                
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
                    Signal(ref CityToRnd.SendFromAToB);
                    cityBums--;
                    return;
                }

                else if (num > researcherPull && num <= researcherPull + factoryWorkerPull && factoryWorkerCapacity > factoryWorkers)
                {
                    //send to factory
                    Signal(ref CityToFactory.SendFromAToB);
                    cityBums--;
                    return;
                }
            }

            if (time % year == 0)
            {
                yearCtr++;
                time = 0;
            }
        }
    }

    class StudentWrapper
    {
        public int timeLeft;
        public StudentWrapper(int t) { timeLeft = t; }
    }
}
