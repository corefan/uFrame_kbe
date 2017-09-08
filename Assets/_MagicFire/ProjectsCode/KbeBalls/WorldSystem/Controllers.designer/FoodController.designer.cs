// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
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
    
    
    public class FoodControllerBase : EntityCommonController {
        
        private uFrame.MVVM.ViewModels.IViewModelManager _FoodViewModelManager;
        
        [uFrame.IOC.InjectAttribute("Food")]
        public uFrame.MVVM.ViewModels.IViewModelManager FoodViewModelManager {
            get {
                return _FoodViewModelManager;
            }
            set {
                _FoodViewModelManager = value;
            }
        }
        
        public IEnumerable<FoodViewModel> FoodViewModels {
            get {
                return FoodViewModelManager.OfType<FoodViewModel>();
            }
        }
        
        public override void Setup() {
            base.Setup();
            // This is called when the controller is created
        }
        
        public override void Initialize(uFrame.MVVM.ViewModels.ViewModel viewModel) {
            base.Initialize(viewModel);
            // This is called when a viewmodel is created
            this.InitializeFood(((FoodViewModel)(viewModel)));
        }
        
        public virtual FoodViewModel CreateFood() {
            return ((FoodViewModel)(this.Create(Guid.NewGuid().ToString())));
        }
        
        public override uFrame.MVVM.ViewModels.ViewModel CreateEmpty() {
            return new FoodViewModel(this.EventAggregator);
        }
        
        public virtual void InitializeFood(FoodViewModel viewModel) {
            // This is called when a FoodViewModel is created
            FoodViewModelManager.Add(viewModel);
        }
        
        public override void DisposingViewModel(uFrame.MVVM.ViewModels.ViewModel viewModel) {
            base.DisposingViewModel(viewModel);
            FoodViewModelManager.Remove(viewModel);
        }
    }
}