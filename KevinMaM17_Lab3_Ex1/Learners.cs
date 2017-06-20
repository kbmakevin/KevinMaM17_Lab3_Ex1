﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using KevinDBModel;
using System.Data.Entity;

namespace KevinMaM17_Lab3_Ex1
{
    public partial class Learners : Form
    {
        public Learners()
        {
            InitializeComponent();
        }

        //Entity Framework DbContext
        private KevinDBEntities dbContext = new KevinDBEntities();

        //load data from database into the form
        private void Learners_Load(object sender, EventArgs e)
        {
            //enables the save button
            kevinTBBindingNavigatorSaveItem.Enabled = true;

            //load KevinTB table ordered by learnerID
            dbContext.KevinTBs
                .OrderBy(learner => learner.learnerID)
                .Load();

            //specify DataSource for kevinTBBindingSource
            kevinTBBindingSource.DataSource = dbContext.KevinTBs.Local;
        }

        private void kevinTBBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            Validate(); //validate the input fields
            kevinTBBindingSource.EndEdit();

            //try to save the changes
            try
            {
                dbContext.SaveChanges();//write changes to underlying db
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException)
            {
                MessageBox.Show("Learner ID, Learner Name and Enrolled Programs must contain values", "Entity Validation Exception");
            }
        }
    }
}