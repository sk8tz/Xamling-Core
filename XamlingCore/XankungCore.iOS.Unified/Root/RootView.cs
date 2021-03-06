﻿using System;
using UIKit;
using XamlingCore.Portable.Contract.Services;
using XamlingCore.Portable.Model.Orientation;

namespace XamlingCore.iOS.Unified.Root
{
    public class RootViewController : UIViewController
    {
        private readonly IOrientationService _orientationService;
        private UIViewController _controller;
        private UIWindow _window;
        public RootViewController(IOrientationService orientationService)
        {
            _orientationService = orientationService;

            _orientationService.OrientationChanged += _orientationService_OrientationChanged;
            _orientationService.SupportedOrientationChanged += _orientationService_SupportedOrientationChanged;
        }

        public void SetChild(UIViewController controller, UIWindow window)
        {
            if (_controller != null)
            {
                _controller.View.RemoveFromSuperview();
                _controller.RemoveFromParentViewController();
            }

            _controller = controller;
            _window = window;

            AddChildViewController(_controller);
            View.AddSubview(_controller.View);
        }

        void _orientationService_SupportedOrientationChanged(object sender, EventArgs e)
        {
        
        }

        void _orientationService_OrientationChanged(object sender, EventArgs e)
        {
           
        }

        

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            switch (_orientationService.SupportedPageOrientation)
            {
                case XSupportedPageOrientation.Both:
                    return UIInterfaceOrientationMask.All;
                    
                    case XSupportedPageOrientation.Landscape:
                    return UIInterfaceOrientationMask.Landscape;
                    
                    case XSupportedPageOrientation.Portrait:
                    return UIInterfaceOrientationMask.Portrait;
                default:
                    return UIInterfaceOrientationMask.Portrait;
            }
        }
    }
}
