﻿using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace TestMVVM.ViewModel
{
    public partial class MainWindowVM : INotifyPropertyChanged
    {
        //for any xaml control use control event to invoke a relay command
        //<ie:Interaction.Triggers>
        //    <ie:EventTrigger EventName = "SelectionChanged" >
        //        <ie:InvokeCommandAction Command = "{Binding SetNameCommand}"/> / this was breaking after deleting selected list item-->
        //    </ie:EventTrigger>
        //</ie:Interaction.Triggers>

        private TagViewModel _tagViewModel;
        public TagViewModel TagViewModel
        {
            get { return _tagViewModel; }
            set { _tagViewModel = value; }
        }

        private EntryViewModel _entryViewModel;
        public EntryViewModel EntryViewModel
        {
            get { return _entryViewModel; }
            set { _entryViewModel = value; }
        }


        public MainWindowVM()
        {           
            TagViewModel = new TagViewModel();
            EntryViewModel = new EntryViewModel();

            LoadData();
        }

        //======================
        private void LoadData()
        {
            DataTable dt1 = dbClass.Execute_Proc("dbo.GetTags");
            foreach (DataRow Row1 in dt1.Rows)
            {
                Tag tag = new Tag
                    (
                    Row1.Field<int?>("TagID"),
                    Row1.Field<int?>("ParentCategoryID"),
                    Row1.Field<string>("Name"),
                    Row1.Field<string>("ColorID"),
                    Row1.Field<bool?>("IsUserCreated"),
                    Row1.Field<bool?>("IsFeelingGoodBad")
                    );

                if (tag.ParentCategoryID == null)
                    TagViewModel.CategoryTagList.Add(tag);
                else
                    TagViewModel.SubcategoryTagList.Add(tag);
            }

            DataTable dt2 = dbClass.Execute_Proc("dbo.GetFoodEntrys");
            foreach (DataRow Row2 in dt2.Rows)
            {
                FoodEntry foodEntry = new FoodEntry
                    (
                    Row2.Field<int?>("FoodEntryId"),
                    Row2.Field<string>("Picture"),
                    Row2.Field<DateTime>("DateTime"),
                    Row2.Field<string>("Description"),
                    Row2.Field<bool?>("IsFeelingGoodBad")
                    );

                DataTable dt3 = dbClass.GetFoodEntry_Tags("dbo.GetFoodEntry_Tags", (int)foodEntry.FoodEntryID);
                foreach (DataRow Row3 in dt3.Rows)
                {
                    int TagID = Row3.Field<int>("TagID");
                    Tag foundTag = TagViewModel.SubcategoryTagList.Where(tag => TagID == tag.TagID).FirstOrDefault();
                    foodEntry.FoodEntry_TagList.Add(foundTag);
                }

                EntryViewModel.FoodEntryList.Add(foodEntry);
            }
        }

        //========================================================
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
