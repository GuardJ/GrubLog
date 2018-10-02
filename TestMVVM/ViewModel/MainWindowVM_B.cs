using GalaSoft.MvvmLight.Command;
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
        #region FoodEntrys and FoodEntry Logic

        

        #region FoodEntry Commands

        public void AddNewFoodEntry()
        {
            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;

            int newFoodEntryID = dbClass.InsertFoodEntryToDB(dateTime, NewEntryDescription, NewEntryIsFeelingGoodBad);

            FoodEntry newFoodEntry = new FoodEntry
            {
                FoodEntryID = newFoodEntryID,
                DateTime = dateTime,
                Description = NewEntryDescription,
                IsFeelingGoodBad = NewEntryIsFeelingGoodBad
            };

            FoodEntryList.Add(newFoodEntry);
        }

        public void DeleteFoodEntry() // Deletes anything related to the FoodEntry. See Stored Procedure.
        {
            dbClass.DeleteFoodEntryFromDB_AtFoodEntryID(SelectedFoodEntryListItem.FoodEntryID.Value);

            FoodEntry currentFoodEntry = FoodEntryList.Where(tag => SelectedFoodEntryListItem.FoodEntryID.Value == tag.FoodEntryID).FirstOrDefault();

            FoodEntryList.Remove(currentFoodEntry);
        }

        public void AddFoodEntry_Tag()
        {
            FoodEntry currentFoodEntry = FoodEntryList.Where(tag => SelectedFoodEntryListItem.FoodEntryID.Value == tag.FoodEntryID).FirstOrDefault();

            Tag currentTag = TagList.Where(tag => SelectedTagListItem.TagID.Value == tag.TagID).FirstOrDefault();

            dbClass.InsertFoodEntry_TagToDB((int)currentFoodEntry.FoodEntryID, (int)currentTag.TagID.Value);

            currentFoodEntry.FoodEntry_TagList.Add(currentTag);
        }

        public void DeleteFoodEntry_Tag()
        {
            FoodEntry currentFoodEntry = FoodEntryList.Where(tag => SelectedFoodEntryListItem.FoodEntryID.Value == tag.FoodEntryID).FirstOrDefault();

            Tag currentTag = TagList.Where(tag => SelectedFoodEntry_TagListItem.TagID.Value == tag.TagID).FirstOrDefault();

            dbClass.DeleteFoodEntry_TagFromDB_AtTagID((int)currentFoodEntry.FoodEntryID.Value, (int)currentTag.TagID.Value);

            currentFoodEntry.FoodEntry_TagList.Remove(currentTag);
        }

        #endregion //Commands

        #region FoodEntry Props

        private Tag _SelectedFoodEntry_TagListItem;
        public Tag SelectedFoodEntry_TagListItem
        {
            get { return _SelectedFoodEntry_TagListItem; }
            set
            {
                _SelectedFoodEntry_TagListItem = value;
                OnPropertyChanged("SelectedFoodEntry_TagListItem");
            }
        }
        //======================
        private FoodEntry _SelectedFoodEntryListItem;
        public FoodEntry SelectedFoodEntryListItem
        {
            get { return _SelectedFoodEntryListItem; }
            set
            {
                _SelectedFoodEntryListItem = value;
                OnPropertyChanged("SelectedFoodEntryListItem");
            }
        }

        //======================
        private bool? _NewEntryIsFeelingGoodBad;
        public bool? NewEntryIsFeelingGoodBad
        {
            get { return _NewEntryIsFeelingGoodBad; }
            set
            {
                _NewEntryIsFeelingGoodBad = value;
                OnPropertyChanged("NewEntryIsFeelingGoodBad");
            }
        }
        
        //======================

        private string _NewEntryDescription;
        public string NewEntryDescription
        {
            get { return _NewEntryDescription; }
            set
            {
                _NewEntryDescription = value;
                OnPropertyChanged("NewEntryDescription");
            }
        }        

        #endregion //Props

        //======================
        private ObservableCollection<FoodEntry> _FoodEntryList;
        public ObservableCollection<FoodEntry> FoodEntryList
        {
            get { return _FoodEntryList; }
            set
            {
                _FoodEntryList = value;
                OnPropertyChanged("FoodEntryList");
            }
        }

        #endregion //FoodEntrys and FoodEntry Logic
    }
}