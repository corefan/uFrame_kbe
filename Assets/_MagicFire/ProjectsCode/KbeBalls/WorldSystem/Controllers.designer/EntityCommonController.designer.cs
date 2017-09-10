// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace KbeBalls {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    
    
    public class EntityCommonControllerBase : uFrame.MVVM.Controller {
        
        private uFrame.MVVM.ViewModels.IViewModelManager<EntityCommonViewModel> _EntityCommonViewModelManager;
        
        [uFrame.IOC.InjectAttribute("EntityCommon")]
        public uFrame.MVVM.ViewModels.IViewModelManager<EntityCommonViewModel> EntityCommonViewModelManager {
            get {
                return _EntityCommonViewModelManager;
            }
            set {
                _EntityCommonViewModelManager = value;
            }
        }
        
        public IEnumerable<EntityCommonViewModel> EntityCommonViewModels {
            get {
                return (EntityCommonViewModelManager as uFrame.MVVM.ViewModels.IViewModelManager<EntityCommonViewModel>).ViewModels;
            }
        }
        
        public override void Setup() {
            base.Setup();
            // This is called when the controller is created
        }
        
        public override void Initialize(uFrame.MVVM.ViewModels.ViewModel viewModel) {
            base.Initialize(viewModel);
            // This is called when a viewmodel is created
            this.InitializeEntityCommon(((EntityCommonViewModel)(viewModel)));
        }
        
        public virtual EntityCommonViewModel CreateEntityCommon() {
            return ((EntityCommonViewModel)(this.Create(Guid.NewGuid().ToString())));
        }
        
        public override uFrame.MVVM.ViewModels.ViewModel CreateEmpty() {
            return new EntityCommonViewModel(this.EventAggregator);
        }
        
        public virtual void InitializeEntityCommon(EntityCommonViewModel viewModel) {
            // This is called when a EntityCommonViewModel is created
            EntityCommonViewModelManager.Add(viewModel);
        }
        
        public override void DisposingViewModel(uFrame.MVVM.ViewModels.ViewModel viewModel) {
            base.DisposingViewModel(viewModel);
            EntityCommonViewModelManager.Remove(viewModel);
        }
    }
}
