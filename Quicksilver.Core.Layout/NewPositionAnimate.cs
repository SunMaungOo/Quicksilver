﻿// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2011. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 17/267 Nepean Hwy, 
//  Seaford, Vic 3198, Australia and are supplied subject to licence terms.
// 
//  Version 1.0.8.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;

namespace ComponentFactory.Quicksilver.Layout
{
    /// <summary>
    /// Defines the starting location/size of a new element.
    /// </summary>
    public class NewPositionAnimate : BoundsEasingAnimate
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the NewPositionAnimate class.
        /// </summary>
        public NewPositionAnimate()
            : base(MetaElementStatus.New)
        {
        }
        #endregion

        #region Public
        /// <summary>
        /// Perform animation effects on the set of children.
        /// </summary>
        /// <param name="animateId">Identifier of the animate to be used.</param>
        /// <param name="metaPanel">Reference to owning panel instance.</param>
        /// <param name="stateDict">Dictionary of per-element state.</param>
        /// <param name="elements">Collection of elements to be animated.</param>
        /// <param name="elapsedMilliseconds">Elapsed milliseconds since last animation cycle.</param>
        public override void ApplyAnimation(string animateId,
                                            MetaPanelBase metaPanel,
                                            MetaElementStateDict stateDict,
                                            ICollection elements,
                                            double elapsedMilliseconds)
        {
            // Only apply if we match the incoming animate identifier
            if (string.IsNullOrEmpty(Id) || Id.Equals(animateId))
            {
                foreach (UIElement element in elements)
                {
                    MetaElementState elementState = stateDict[element];

                    // If this is a new element that has not had its current rectangle set
                    if ((elementState.Status == MetaElementStatus.New) && elementState.NewCalculating)
                    {
                        elementState.CurrentRect = RectFromSize(Size, RectFromLocation(Location, metaPanel, elementState), elementState);
                        elementState.TargetChanged = true;
                        elementState.AnimateComplete = false;
                    }
                }
            }

            // Let base class take care of easing animations
            base.ApplyAnimation(animateId, metaPanel, stateDict, elements, elapsedMilliseconds);
        }
        #endregion
    }
}
