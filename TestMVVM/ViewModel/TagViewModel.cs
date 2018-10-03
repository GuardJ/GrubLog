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
    public class TagViewModel : INotifyPropertyChanged
    {
        #region Properties
        private ObservableCollection<Tag> _subCategotyTagList;
        public ObservableCollection<Tag> SubcategoryTagList
        {
            get { return _subCategotyTagList; }
            set
            {
                _subCategotyTagList = value;
                OnPropertyChanged("SubcategoryTagList");
            }
        }

        private ObservableCollection<Tag> _categoryTagList;
        public ObservableCollection<Tag> CategoryTagList
        {
            get { return _categoryTagList; }
            set
            {
                _categoryTagList = value;
                OnPropertyChanged("CategoryTagList");
            }
        }

        private ObservableCollection<Tag> _subCategoryTagView;
        public ObservableCollection<Tag> SubCategoryTagView
        {
            get { return _subCategoryTagView; }
            set
            {
                _subCategoryTagView = value;
                OnPropertyChanged("SubCategoryTagView");
            }
        }




        private Tag _selectedParentCategory;
        public Tag SelectedParentCategory
    {
            get { return _selectedParentCategory; }
            set
            {
                _selectedParentCategory = value;
                SubCategoryTagView = new ObservableCollection<Tag>(SubcategoryTagList
                                        .Where(tag => tag.ParentCategoryID == SelectedParentCategory.TagID).ToList()); 
                OnPropertyChanged("SelectedParentCategory");
            }
        }



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

        #region Constructor and Commands

        public RelayCommand AddNewTagCommand { get; set; }
        public RelayCommand AddNewCategoryCommand { get; set; }
        public RelayCommand DeleteTagCommand { get; set; }
        public RelayCommand DeleteCategoryCommand { get; set; }

        public TagViewModel()
        {
            AddNewTagCommand = new RelayCommand(AddNewTag);
            AddNewCategoryCommand = new RelayCommand(AddNewCategory);
            DeleteCategoryCommand = new RelayCommand(DeleteCategory);
            DeleteTagCommand = new RelayCommand(DeleteTag);
            CategoryTagList = new ObservableCollection<Tag>();
            SubcategoryTagList = new ObservableCollection<Tag>();

        }
        #endregion

        #region Methods


        public void AddNewTag()
        {
            int? SelectedParentID = SelectedParentCategory?.TagID;            
            int newTagID = dbClass.InsertTagToDB(SelectedParentID, NewTagName, NewTagIsFeelingGoodBad);

            Tag newTag = new Tag
            {
                TagID = newTagID,
                ParentCategoryID = SelectedParentID,
                Name = NewTagName,
                IsFeelingGoodBad = NewTagIsFeelingGoodBad
            };
            SubcategoryTagList.Add(newTag);
        }

        public void AddNewCategory()
        {
            int newTagID = dbClass.InsertTagToDB(null, NewTagName, NewTagIsFeelingGoodBad);

            Tag newTag = new Tag
            {
                TagID = newTagID,
                ParentCategoryID = null,
                Name = NewTagName,
                IsFeelingGoodBad = NewTagIsFeelingGoodBad
            };
            CategoryTagList.Add(newTag);
        }

        public void DeleteTag()
        {
            dbClass.DeleteTagFromDB_AtTagID(SelectedTagListItem.TagID.Value);

            Tag currentTag = SubcategoryTagList.Where(tag => SelectedTagListItem.TagID.Value == tag.TagID).FirstOrDefault();

            //foreach (FoodEntry foodEntry in FoodEntryList)
            //{
            //    foreach (Tag tag in foodEntry.FoodEntry_TagList)
            //    {
            //        if (tag == currentTag)
            //        {
            //            foodEntry.FoodEntry_TagList.Remove(tag);
            //            break;
            //        }
            //    }
            //}            

            SubcategoryTagList.Remove(currentTag);
        }
        public void DeleteCategory()
        {
            dbClass.DeleteTagFromDB_AtTagID(SelectedParentCategory.TagID.Value);

            Tag currentTag = CategoryTagList.Where(tag => SelectedParentCategory.TagID.Value == tag.TagID).FirstOrDefault();
            CategoryTagList.Remove(currentTag);
        }

        #endregion
        
        #region NotifyPropertyChanged
        //========================================================
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

    }
}
