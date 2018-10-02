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
        #region Tags and Tag Logic  

        #region Tag Commands

        public void AddNewTag()
        {
            int? SelectedParentID;
            if (StoredTagSelection != null)
                SelectedParentID = StoredTagSelection.TagID.Value;
            else
                SelectedParentID = null;

            int newTagID = dbClass.InsertTagToDB(SelectedParentID, NewTagName, NewTagIsFeelingGoodBad);

            Tag newTag = new Tag
            {
                TagID = newTagID,
                ParentCategoryID = SelectedParentID,
                Name = NewTagName,
                IsFeelingGoodBad = NewTagIsFeelingGoodBad
            };

            TagList.Add(newTag);

            StoredTagSelection = null;
        }

        public void DeleteTag()
        {
            dbClass.DeleteTagFromDB_AtTagID(SelectedTagListItem.TagID.Value);

            Tag currentTag = TagList.Where(tag => SelectedTagListItem.TagID.Value == tag.TagID).FirstOrDefault();

            foreach (FoodEntry foodEntry in FoodEntryList)
            {
                foreach (Tag tag in foodEntry.FoodEntry_TagList)
                {
                    if (tag == currentTag)
                    {
                        foodEntry.FoodEntry_TagList.Remove(tag);
                        break;
                    }
                }
            }            

            TagList.Remove(currentTag);
        }

        public void StoreTag()
        {
            StoredTagSelection = SelectedTagListItem;
            //if(IsRBChecked == true)
            //{
            //    IsRBChecked = false;
            //    return;
            //}
            //IsRBChecked = true;

        }

        public void ClearStoredTag()
        {
            StoredTagSelection = null;
        }

        #endregion

        #region Tag Props


        public Tag StoredTagSelection { get; set; }
        
        //======================
        private Tag _SelectedTagListItem;
        public Tag SelectedTagListItem
        {
            get { return _SelectedTagListItem; }
            set
            {
                _SelectedTagListItem = value;
                OnPropertyChanged("SelectedTagListItem");
            }
        }

        //=====================
        //private bool _IsRBChecked;
        //public bool IsRBChecked
        //{
        //    get { return _IsRBChecked; }
        //    set
        //    {
        //        _IsRBChecked = value;
        //        OnPropertyChanged("IsRBChecked");
        //    }
        //}

        //=====================
        private bool? _NewTagIsFeelingGoodBad;
        public bool? NewTagIsFeelingGoodBad
        {
            get { return _NewTagIsFeelingGoodBad; }
            set
            {
                _NewTagIsFeelingGoodBad = value;
                OnPropertyChanged("NewIsFeelingGoodBad");
            }
        }

        //======================

        private string _NewTagName;
        public string NewTagName
        {
            get { return _NewTagName; }
            set
            {
                _NewTagName = value;
                OnPropertyChanged("NewTagName");
            }
        }

        #endregion

        //======================
        private ObservableCollection<Tag> _TagList;
        public ObservableCollection<Tag> TagList
        {
            get { return _TagList; }
            set
            {
                _TagList = value;
                OnPropertyChanged("TagList");
            }
        }

        #endregion        
    }
}
