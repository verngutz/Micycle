﻿using Microsoft.Xna.Framework;
using MiUtil;

namespace Micycle
{
    public class MicycleGameSystem : MiComponent
    {
        private int cityPeople;
        private int schoolStudents;
        private int schoolTeachers;
        private int researchers;
        private int factoryWorkers;
        private int cityBums;

        private int factoryWorkerCapacity;
        private int researcherCapacity;

        private int educationBudget;
        private int workerWage;
        private int researcherWage;

        private int time;

        private int cityMoney;
        private int ownerMoney;
        private int researchPoints;

        public MicycleGameSystem(Micycle game)
            : base(game)
        {

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
            workerWage += dx;
            if (workerWage < 0) workerWage = 0;
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
            //update the in flow and out flow from city (bums + "kids")
            //update money
        }

        public void updateFactory()
        {
            //update owner money
            //update the in flow and out flow from factory (workers)
        }

        public void updateSchool()
        {
            //update owner money
            //update the in flow and out flow from school (students)
        }

        public void updateResearchCenter()
        {
            //update owner money
            //update the in flow and out flow from research center (research center + teachers(?))
            //update research points
        }

        public override void Update(GameTime gameTime)
        {
            time++;
        }
    }
}