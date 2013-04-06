using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

/*
 * C# Wrapper around the Oscilloscope DLL
 * 
 * (C)2006 Dustin Spicuzza
 *
 * This library interface is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; only
 * version 2.1 of the License.
 * 
 * Some comments/function declarations taken from the original 
 * "oscilloscope-lib" documentation.
 * 
 * Oscilloscope Properties added by: Frank Greenlee, April, 2010
 * MessageBoxes replaced with Exceptions by: Sebastian Madgwick, February, 2011
 * "Osc_DLL.dll" replaced with Osc_DLL_path by: Sebastian Madgwick, May, 2011
 * 
 */

/************************************************************************
*
*  Quick Reference: Publicly Available Methods and Properties 
*
*************************************************************************

*************************************************************************
*
*  Public Methods
*
*************************************************************************

   Method: CreateScope() 
   
   Creates an instance of the Oscilloscope. The Initialization file 
   is not used in this version of the method.
  
   Lifetime:   static
   Parameters: void
   Return:     reference of type Oscilloscope to the instance if successful
               null if not successful

*************************************************************************

   Method: CreateScope ( string, string )
 
   Creates an instance of the Oscilloscope. The Initialization file 
   is used (if found) in this version of the method.

   Lifetime:   static
   Parameters: string: Filenmae of intialization file
               string: Filename suffix
   Return:     reference of type Oscilloscope to the instance if successful
               null if not successful

*************************************************************************

   Method: ShowScope ()
  
   Displays the Oscilloscope. After the instance is created it will not 
   become visible until this method is called.
   
   Lifetime:   instance
   Parameters: void
   Return:     void

*************************************************************************

   Method: HideScope ()
 
   Makes the Oscilloscope invisible non-destructively.
  
   Lifetime:   instance
   Parameters: void
   Return:     void

*************************************************************************

   Method: ClearScope ()  
   
   Clears the Oscilloscope's buffers.
  
   Lifetime:   instance
   Parameters: void
   Return:     void

*************************************************************************

   Method: AddScopeData ( double, double, double )  
   
   Adds one new data point to each of the three traces.
  
   Lifetime:   instance
   Parameters: double: 3X New data points, one for each trace.
   Return:     void

*************************************************************************

   Method: AddExternalScopeData ( double )  
   
   Sends a new data point to the external trigger input, which is used 
   for triggering when the Oscilloscope's Trigger Source is set to 
   EXTERNAL_TRIG.
  
   Lifetime:   instance
   Parameters: double: External Trigger Data Point
   Return:     void

*************************************************************************

   Method: UpdateScope ()  
   
   Quickly updates the Oscilloscope traces, for use when there is a gap
   in the dta stream.
  
   Lifetime:   instance
   Parameters: void
   Return:     void

*************************************************************************

   Method: Dispose ()
 
   Releases the Oscilloscope resources back to the system and removes
   the Oscilloscope from the display. Also sets the _disposed flag to
   true. Any future calls or property accesses will be ignored and 
   will set a SCOPE_DISPOSED_ERROR into LastError.

   Lifetime:   instance
   Parameters: void
   Return:     void
  
*************************************************************************
*
*  Public Properties -- all of these are class instance members.
*
*************************************************************************
    
   Property: LastError
    
   An application can check this to see if there has been an 
   error, even when error message displays are turned off. 
   If it is non-zero (!=NO_ERROR), there has been an error 
   and the value refers to one of the listed error constants.
    
   When setting LastError the value is ignored and LastError
   is set to its defaul value, NO_ERROR. The application
   should do this after finding a non-zero error value, as it is 
   not done automatically; the error code value will persist 
   until it is manually reset.
   
   Value:   int, equal to one of the error constants
   Access:  Read/Write (any write resets LastError to NO_ERROR
   Default: 0 (NO_ERROR)
    
**************************************************************************

   Property: ErrorMessagesOn
     
   If set to "true", a throw new Exception will appear when there is an error. 
   When debugging is completed it may be desirable to set ErrorMessagesOn 
   to "false."  The LastError variable will work regardless of this 
   setting and may be checked even when there is no instance of the 
   Oscilloscope class.
     
   Value:   bool
   Access:  Read/Write
   Default: true

**************************************************************************

   Property: ControlPanelVisible
    
   Displays or hides the control panel at the bottom of the 
   oscilloscope display.
     
   Value:   bool
   Access:  Read/Write
   Default: true (also can be define in the intialization 
            file, if it is used)

**************************************************************************

   Property: Left
     
   Sets the horizontal position of the oscilloscope on the screen, 
   specified as the number of pixels from the left edge of the screen
   to the left edge of the oscilloscope form.
    
   Value:   int, Range: 0-Current Screen Width
   Access:  Read/Write    
   Default: Somewhere on the Screen (also can be defined in the 
            intialization file, if it is used)

**************************************************************************

   Property: Top
     
   Sets the vertical position of the oscilloscope on the screen, 
   specified as the number of pixels from the top edge of the screen
   to the top edge of the oscilloscope form.
    
   Value:   int, Range: 0-Current Screen Height
   Access:  Read/Write
   Default: Somewhere on the Screen (also can be defined in 
            the intialization file, if it is used)

**************************************************************************

   Property: Width
     
   Sets the Width of the oscilloscope form in pixels.
    
   Value:   int, Range: 40-Current Screen Width
   Access:  Read/Write
   Default: (->fill this in) (also can be defined in the 
            initialization file, if it is used)

**************************************************************************

   Property: Height
     
   Sets the Height of the oscilloscope form in pixels.
    
   Value:   int, Range: 40-Current Screen Height
   Access:  Read/Write
   Default: (->fill this in) (also can be defined in the 
            initialization file, if used)
  
**************************************************************************

   Property: CellSize
    
   Sets the size of a grid cell in pixels; sets both height
   and width of grid cells to this single property setting.
    
   Value:   int, Range: 10-150
   Access:  Read/Write
   Default: 40 (also can be defined in the initialization file, if it is 
            used)
 
**************************************************************************

   Property: SamplesPerGridCell
     
   Sets the number of samples per grid cell on the horizontal (time) axis.
   The value will be adjusted to closest valid number (see the DLL 
   documentation).
     
   WARNING: if this setting is too low and the data stream is too fast
   a buffer overrun can be caused, which will have no indicator other
   than the DLL locking up.

   Value:   double, Range 0.10000-20,000
   Access:  Read/Write
   Default: (->fill this in) (also can be defined in the 
            initialization file, if it is used)
 
**************************************************************************

   Property: Caption
     
   Sets the caption at the top of the oscilloscope form.
     
   Value:   string, Range 0-250 characters
   Access:  Write Only
   Default: "Oscilloscope" (also can be defined in the 
            initialization file, if it is used)

**************************************************************************

   Property: AmplitudeScale1
     
   Sets the Amplitude Scale of Trace 1's grid divisions, in binary steps/grid 
   cell. The value will be adjusted to closest valid number (see the DLL 
   documentation).
     
   Value:   double, Range: 0.00001-100,000.00000
   Access:  Read/Write
   Default: (->fill this in) (also can be defined in the 
            initialization file, if it is used)
  
**************************************************************************

   Property: AmplitudeScale2
     
   Sets the Amplitude Scale Trace 2's grid divisions, in binary steps/grid 
   cell. The value will be adjusted to closest valid number (see the DLL 
   documentation).
     
   Value:   double, Range: 0.00001-100,000.00000
   Access:  Read/Write
   Default: (->fill this in) (also can be defined in the 
            initialization file, if it is used)

**************************************************************************

   Property: AmplitudeScale3
     
   Sets the Amplitude Scale Trace 3's grid divisions, in binary steps/grid 
   cell. The value will be adjusted to closest valid number (see the DLL 
   documentation).
     
   Value:   double, Range: 0.00001-100,000.00000
   Access:  Read/Write
   Default: (->fill this in) (also can be defined in the 
            initialization file, if it is used)

**************************************************************************

   Property: VerticalOffset1
     
   Sets the Vertical Offset of Trace 1, in units of of Trace 1's
   AmplitudeScale.
     
   Value:   double, Range: -1,000,000.00000-+1,000,000.00000
   Access:  Read/Write
   Default: 0.00000 (also can be defined in the initialization file, 
            if it is used)

**************************************************************************

   Property: VerticalOffset2
     
   Sets the Vertical Offset of Trace 2, in units of of Trace 2's
   AmplitudeScale.
     
   Value:   double, Range: -1,000,000.00000-+1,000,000.00000
   Access:  Read/Write
   Default: 0.00000 (also can be defined in the initialization file, 
            if it is used)

**************************************************************************

   Property: VerticalOffset3
     
   Sets the Vertical Offset of Trace 3, in units of of Trace 3's
   AmplitudeScale.
     
   Value:   double, Range: -1,000,000.00000-+1,000,000.00000
   Access:  Read/Write
   Default: 0.00000 (also can be defined in the initialization file, 
            if it is used)

**************************************************************************

   Property: TriggerLevel1
     
   Sets the Trigger Offset of Trace 1, in units of Trace 1's AmplitudeScale.
     
   Value:   double, Range: -1,000,000.00000-+1,000,000.00000
   Access:  Read/Write
   Default: 0.00000 (also can be defined in the initialization file, if 
            it is used)
 
**************************************************************************

   Property: TriggerLevel2
     
   Sets the Trigger Offset of Trace 2, in units of Trace 2's AmplitudeScale.
     
   Value:   double, Range: -1,000,000.00000-+1,000,000.00000
   Access:  Read/Write
   Default: 0.00000 (also can be defined in the initialization file, if 
            it is used)

**************************************************************************

   Property: TriggerLevel3
     
   Sets the Trigger Offset of Trace 3, in units of Trace 3's AmplitudeScale.
     
   Value:   double, Range: -1,000,000.00000-+1,000,000.00000
   Access:  Read/Write
   Default: 0.00000 (also can be defined in the initialization file, if 
            it is used)
  
**************************************************************************

   Property: TriggerSource
     
   Sets the Oscilloscope's Trigger Source. The constants TRACE1_TRIG,
   TRACE2_TRIG, TRACE3_TRIG and EXTERNAL_TRIG are provided for conenience.
     
   Value:   int, Range 0-3 (TRACE1_TRIG-EXTERNAL_TRIG)
   Access:  Read/Write
   Default: TRACE1_TRIG (also can be defined in the initialization file, if 
            it is used)

**************************************************************************

   Property: TriggerEdge
     
   Sets the Trigger Edge Detection Mode. Any valid negative int sets it 
   Negative Edge Triggered, any valid positive int sets it Positive Edge 
   Triggered, and 0 set it to disabled. The constants FALLING_EDGE_TRIG,
   RISING_EDGE_TRIG and DISABLED_TRIG are provided for convenience.
     
   Value:   int, Range: see above
   Access:  Read/Write
   Default: DISABLED_TRIG (also can be defined in the initialization file, 
            if it is used)
 
**************************************************************************

   Property: TriggerDelay
     
   Sets the Trigger Delay, in number of samples (see DLL Documentation).
     
   Value:   int, RangeL 0-Any valid positive int value
   Access:  Read/Write
   Default: 0 (also can be defined in the initialization file, if it is 
            used)

**************************************************************************

   Property: TraceDisplayWidth
     
   Returns the width of the current trace display area in pixels.
     
   Value:   int
   Access:  Read Only
   Default: N/A
 
**************************************************************************

   Property: TraceDisplayHeight
     
   Returns the height of the current trace display area in pixels.
     
   Value:   int
   Access:  Read Only
   Default: N/A
  
**************************************************************************
*
*  Miscellaneous Public Resources
*
**************************************************************************

   --> Trigger Source Constants (int's):
   TRACE1_TRIG = 0;
   TRACE2_TRIG = 1;
   TRACE3_TRIG = 2;
   EXTERNAL_TRIG = 3;

   --> Trigger Mode Constants (int's):
   FALLING_EDGE_TRIG = -1; 
   RISING_EDGE_TRIG = 1;
   DISABLED_TRIG = 0;

   --> Error Constants (int's):
   NO_ERROR = 0;
   SCOPE_CREATION_ERROR = 1;
   SCOPE_HANDLE_ERROR = 2;
   SCOPE_DISPOSED_ERROR = 3;
   LEFT_RANGE_ERROR = 4;
   TOP_RANGE_ERROR = 5;
   WIDTH_RANGE_ERROR = 6;
   HEIGHT_RANGE_ERROR = 7;
   CELLSIZE_RANGE_ERROR = 8;
   SAMPLESPERGRIDCELL_RANGE_ERROR = 9;
   CAPTIONSIZE_ERROR = 10;
   CELLAMPLITUDESCALE_RANGE_ERROR = 11;
   VERTICALOFFSET_RANGE_ERROR = 12;
   TRIGGERLEVEL_RANGE_ERROR = 13;
   TRIGGERSOURCE_RANGE_ERROR = 14;
   TRIGGERDELAY_RANGE_ERROR = 15;
   SCOPE_TRIGGERSOURCE_ERROR = 16;
   SCOPE_TRIGGEREDGE_ERROR = 17;
   SCOPE_NOT_VISIBLE = 18;

**************************************************************************/


// Begin Oscilloscope DLL Wrapper Implementation
namespace Serial_Oscilloscope
{
    sealed class Oscilloscope : IDisposable
    {
        const string Osc_DLL_path = "Oscilloscope/Osc_DLL.dll";

        #region External declarations

        /******************************************
         *                                         *
         * Import Essential Oscilloscope Functions *
         *                                         *
         ******************************************/
        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int AtOpenLib
            (int Prm);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeCreate(
            int Prm,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder P_IniName,
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder P_IniSuffix);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeDestroy
            (int ScopeHandle);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeShow
            (int ScopeHandle);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeHide
            (int ScopeHandle);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeCleanBuffers
            (int ScopeHandle);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ShowNext
            (int ScopeHandle,
            double[] PArrDbl);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ExternalNext
            (int ScopeHandle,
            ref double PDbl);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int QuickUpDate
            (int ScopeHandle);

        /******************************************
         *                                        *
         * Import Oscilloscope Attribute Controls *
         *                                        *
         * ****************************************
         * 
         * Import Set functions:
        */

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeSetPanelState
          (int ScopeHandle, int VizState);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeSetFormPos
          (int ScopeHandle, int FormLeft, int FormTop);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeSetFormSize
          (int ScopeHandle, int FormWidth, int FormHeight);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeSetCellPixelSize
          (int ScopeHandle, int CellPixelSize);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeSetCellSampleSize
          (int ScopeHandle, double CellSampleSize);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeSetCaption
          (int ScopeHandle,
          [MarshalAs(UnmanagedType.LPStr)] StringBuilder P_ZeroTermStr);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeSetAmpScale
          (int ScopeHandle, int BeamNum, double AmpScale);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeSetAmpOffset
          (int ScopeHandle, int BeamNum, double AmpOffset);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeSetTriggerLevel
          (int ScopeHandle, int BeamNum, double TriggerLevel);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeSetTriggerSourse
          (int ScopeHandle, int BeamNum);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeSetActiveTriggerEdge
          (int ScopeHandle, int TriggerEdge);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeSetTrigHoldOffSmplsNum
          (int ScopeHandle, int HoldOffVsl);

        /* 
         * Import Get functions:
         */

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeGetFormLeft
          (int ScopeHandle);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeGetFormTop
          (int ScopeHandle);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeGetFormWidth
          (int ScopeHandle);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeGetFormHeight
          (int ScopeHandle);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeGetScreenWidth
          (int ScopeHandle);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeGetScreenHeight
          (int ScopeHandle);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeGetCellPixelSize
          (int ScopeHandle);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern double ScopeGetCellSampleSize
          (int ScopeHandle);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeGetPanelState
          (int ScopeHandle);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern double ScopeGetAmpScale
          (int ScopeHandle, int Beam);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern double ScopeGetAmpOffset
          (int ScopeHandle, int Beam);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern double ScopeGetTriggerLevel
          (int ScopeHandle, int Beam);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeGetTriggerSourse
          (int ScopeHandle);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeGetActiveTriggerEdge
          (int ScopeHandle);

        [DllImport(Osc_DLL_path, CallingConvention = CallingConvention.Cdecl)]
        private static extern int ScopeGetTrigHoldOffSmplsNum
          (int ScopeHandle);

        #endregion

        // Property Control Constants (provided for convenience)
        //
        // Trigger Source Constants 
        public const int TRACE1_TRIG = 0, TRACE2_TRIG = 1, TRACE3_TRIG = 2, EXTERNAL_TRIG = 3;
        //
        // Trigger Type Constants
        public const int FALLING_EDGE_TRIG = -1, RISING_EDGE_TRIG = 1, DISABLED_TRIG = 0;

        /// <summary>
        /// Error Constants
        /// </summary>
        public const int NO_ERROR = 0;
        public const int SCOPE_CREATION_ERROR = 1;
        public const int SCOPE_HANDLE_ERROR = 2;
        public const int SCOPE_DISPOSED_ERROR = 3;
        public const int LEFT_RANGE_ERROR = 4;
        public const int TOP_RANGE_ERROR = 5;
        public const int WIDTH_RANGE_ERROR = 6;
        public const int HEIGHT_RANGE_ERROR = 7;
        public const int CELLSIZE_RANGE_ERROR = 8;
        public const int SAMPLESPERGRIDCELL_RANGE_ERROR = 9;
        public const int CAPTIONSIZE_ERROR = 10;
        public const int CELLAMPLITUDESCALE_RANGE_ERROR = 11;
        public const int VERTICALOFFSET_RANGE_ERROR = 12;
        public const int TRIGGERLEVEL_RANGE_ERROR = 13;
        public const int TRIGGERSOURCE_RANGE_ERROR = 14;
        public const int TRIGGERDELAY_RANGE_ERROR = 15;
        public const int SCOPE_TRIGGERSOURCE_ERROR = 16;
        public const int SCOPE_TRIGGEREDGE_ERROR = 17;
        public const int SCOPE_NOT_VISIBLE = 18;

        #region Static members
        static bool initialized = false;

        /// <summary>
        /// Creates a new Oscilloscope. Returns the object if successful,
        /// otherwise returns null. Generally, if it returns null then
        /// it cannot find the correct DLL to load
        /// </summary>
        /// <returns>Oscilloscope instance</returns>
        public static Oscilloscope CreateScope()
        {
            return CreateScope("NO_INI_FILE", "NON");
        }

        /// <summary>
        /// Creates a new Oscilloscope. Returns the object if successful,
        /// otherwise returns null. Generally, if it returns null then
        /// it cannot find the correct DLL to load
        /// </summary>
        /// <param name="IniName">Name of INI file with scope settings</param>
        /// <param name="IniSuffix">Section name suffix (see manual)</param>
        /// <returns>Oscilloscope instance</returns>
        public static Oscilloscope CreateScope(string IniName, string IniSuffix)
        {
            int handle;

            _Scope_Visible = false;
            try
            {
                if (!initialized)
                {
                    // initialize
                    if (AtOpenLib(0) == -1)
                    {
                        ShowError(SCOPE_CREATION_ERROR);
                        return null;
                    }
                    // Set to true
                    initialized = true;
                }
            }
            catch
            {
                // Return the Error
                ShowError(SCOPE_CREATION_ERROR);
                return null;
            }

            // Create the Oscilloscope Instance
            handle = ScopeCreate(0, new StringBuilder(IniName), new StringBuilder(IniSuffix));

            if (handle != 0)
            {
                return new Oscilloscope(handle);
            }
            ShowError(SCOPE_CREATION_ERROR);
            return null;
        }

        #endregion

        private int scopeHandle;
        private bool _disposed = false;

        // Class Constructors
        private Oscilloscope()
        {
        }

        private Oscilloscope(int handle)
        {
            scopeHandle = handle;
        }

        // Class Destructor
        ~Oscilloscope()
        {
            Dispose();
        }

        /// <summary>
        /// Shows the scope.
        /// </summary>
        public void ShowScope()
        {
            if (!_disposed)
            {
                _Scope_Visible = true;
                ScopeShow(scopeHandle);
            }
            else
                ShowError(SCOPE_DISPOSED_ERROR);
        }

        /// <summary>
        /// Hides the scope from view
        /// </summary>
        public void HideScope()
        {
            if (!_disposed)
            {
                _Scope_Visible = false;
                ScopeHide(scopeHandle);
            }
            else
                ShowError(SCOPE_DISPOSED_ERROR);
        }

        /// <summary>
        /// Clears the buffer of the scope
        /// </summary>
        public void ClearScope()
        {
            if (!_disposed)
                ScopeCleanBuffers(scopeHandle);
            else
                ShowError(SCOPE_DISPOSED_ERROR);
        }

        /// <summary>
        /// Add data to the scope traces.
        /// </summary>
        /// <param name="beam1">Data for first beam</param>
        /// <param name="beam2">Data for second beam</param>
        /// <param name="beam3">Data for third beam</param>
        public void AddScopeData(double beam1, double beam2, double beam3)
        {
            if (!_disposed)
            {
                double[] PArrDbl = new double[3];
                PArrDbl[0] = beam1;
                PArrDbl[1] = beam2;
                PArrDbl[2] = beam3;

                ShowNext(scopeHandle, PArrDbl);
            }
            else
                ShowError(SCOPE_DISPOSED_ERROR);
        }

        /// <summary>
        /// Add data to the 'external' trigger function signal. This value is used
        /// when TriggerSource == EXTERNAL_TRIG.
        /// </summary>
        /// <param name="data">The data</param>
        public void AddExternalScopeData(double data)
        {
            if (!_disposed)
                ExternalNext(scopeHandle, ref data);
            else
                ShowError(SCOPE_DISPOSED_ERROR);
        }

        /// <summary>
        /// Quickly refreshes screen of oscilloscope. Calling this function is
        /// not usually required. Recommended for using in situations when 
        /// intensive data stream is going into oscilloscope
        /// </summary>
        public void UpdateScope()
        {
            if (!_disposed)
            {
                QuickUpDate(scopeHandle);
            }
            else
                ShowError(SCOPE_DISPOSED_ERROR);
        }

        /*****************************************
        *                                        *
        * C# wrapper for Oscilloscope Properties * 
        *                                        *
        *****************************************/

        private static bool _ErrorMessagesOn = true;
        private static int _LastError = NO_ERROR;
        private static bool _Scope_Visible = false;

        /// <summary>
        /// Property: LastError
        /// 
        /// An application can check this to see if there has been an 
        /// error, even when error message displays are turned off. 
        /// If it is non-zero (!=NO_ERROR), there has been an error 
        /// and the value refers to one of the listed error constants.
        /// 
        /// When setting LastError the value is ignored and LastError
        /// is set to its defaul value, NO_ERROR. The application
        /// should do this after finding an error value here, as it is 
        /// not done automatically; the error code value will persist 
        /// until it is manually reset.
        ///
        /// Value:   int, equal to one of the error constants
        /// Access:  Read/Write (any write resets LastError to NO_ERROR
        /// Default: 0 (NO_ERROR)
        /// </summary>
        public int LastError
        {
            set { _LastError = NO_ERROR; }
            get { return _LastError; }
        }

        /// <summary>
        /// Property: ErrorMessagesOn
        /// 
        /// If ErrorMessagesOn is set to "true", a throw new Exception will appear 
        /// when there is an error. When debugging is completed it may be 
        /// desirable to set ErrorMessagesOn to "false."
        /// 
        /// Value:   bool
        /// Access:  Read/Write
        /// Default: true
        /// </summary>
        public bool ErrorMessagesOn
        {
            set { _ErrorMessagesOn = value; }
            get { return _ErrorMessagesOn; }
        }

        /// <summary>
        /// Property: ControlPanelVisible
        ///
        /// Displays or hides the control panel at the bottom of the 
        /// oscilloscope display
        /// 
        /// Value:   bool
        /// Access:  Read/Write
        /// Default: true (also can be define in the intialization 
        ///          file, if used)
        /// </summary>
        public bool ControlPanelVisible
        {
            set
            {
                int Ret = 0;

                if (!_disposed)
                {
                    if (value)
                        Ret = ScopeSetPanelState(scopeHandle, 1);
                    else
                        Ret = ScopeSetPanelState(scopeHandle, 0);
                    if (Ret != scopeHandle)
                        ShowError(SCOPE_HANDLE_ERROR);
                }
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
            }

            get
            {
                if (!_disposed)
                {
                    if (ScopeGetPanelState(scopeHandle) == 1)
                        return false;
                    else
                        return true;
                }
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return false;
            }
        }

        /// <summary>
        /// Property: Left
        /// 
        /// Sets the horizontal position of the oscilloscope on the screen, 
        /// specified as the number of pixels from the left edge of the screen
        /// to the left edge of the oscilloscope form.
        ///
        /// Value:   int, Range: 0-Current Screen Width
        /// Access:  Read/Write    
        /// Default: Somewhere on the Screen (also can be defined in the 
        ///          intialization file, if used)
        /// </summary>
        public int Left
        {
            set
            {
                int MonitorWidth, MonitorHeight;

                GetMonitorDimensions(out MonitorWidth, out MonitorHeight);
                if ((value < 0) || (value > MonitorWidth))
                    ShowError(LEFT_RANGE_ERROR, value);
                else
                {
                    if (!_disposed)
                    {
                        int Ret = 0, Top;

                        Top = ScopeGetFormTop(scopeHandle);
                        Ret = ScopeSetFormPos(scopeHandle, value, Top);
                        if (Ret != scopeHandle)
                            ShowError(SCOPE_HANDLE_ERROR);
                    }
                    else
                        ShowError(SCOPE_DISPOSED_ERROR);
                }
            }

            get
            {
                if (!_disposed)
                    return ScopeGetFormLeft(scopeHandle);
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return 0;
            }
        }

        /// <summary>
        /// Property: Top
        /// 
        /// Sets the vertical position of the oscilloscope on the screen, 
        /// specified as the number of pixels from the top edge of the screen
        /// to the top edge of the oscilloscope form.
        ///
        /// Value:   int, Range: 0-Current Screen Height
        /// Access:  Read/Write
        /// Default: Somewhere on the Screen (also can be defined in 
        ///          the intialization file, if used)
        /// </summary>
        public int Top
        {
            set
            {
                int MonitorWidth, MonitorHeight;

                GetMonitorDimensions(out MonitorWidth, out MonitorHeight);
                if ((value < 0) || (value > MonitorHeight))
                    ShowError(TOP_RANGE_ERROR, value);
                else
                {
                    if (!_disposed)
                    {
                        int Ret = 0, Left;

                        Left = ScopeGetFormLeft(scopeHandle);
                        Ret = ScopeSetFormPos(scopeHandle, Left, value);
                        if (Ret != scopeHandle)
                            ShowError(SCOPE_HANDLE_ERROR);
                    }
                    else
                        ShowError(SCOPE_DISPOSED_ERROR);
                }
            }

            get
            {
                if (!_disposed)
                    return ScopeGetFormTop(scopeHandle);
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return 0;
            }
        }

        /// <summary>
        /// Property: Width
        /// 
        /// Sets the Width of the oscilloscope form in pixels.
        ///
        /// Value:   int, Range: 40-Current Screen Width
        /// Access:  Read/Write
        /// Default: (->fill this in) (also can be defined in the 
        ///          initialization file, if used)
        /// </summary>
        public int Width
        {
            set
            {
                int MonitorWidth, MonitorHeight;

                GetMonitorDimensions(out MonitorWidth, out MonitorHeight);
                if ((value < 40) || (value > MonitorWidth))
                    ShowError(WIDTH_RANGE_ERROR, value);
                else
                {
                    if (!_disposed)
                    {
                        int Ret = 0, Height;

                        Height = ScopeGetFormHeight(scopeHandle);
                        Ret = ScopeSetFormSize(scopeHandle, value, Height);
                        if (Ret != scopeHandle)
                            ShowError(SCOPE_HANDLE_ERROR);
                    }
                    else
                        ShowError(SCOPE_DISPOSED_ERROR);
                }
            }

            get
            {
                if (!_disposed)
                    return ScopeGetFormWidth(scopeHandle);
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return 0;
            }
        }

        /// <summary>
        /// Property: Height
        /// 
        /// Sets the Height of the oscilloscope form in pixels.
        ///
        /// Value:   int, Range: 40-Current Screen Height
        /// Access:  Read/Write
        /// Default: (->fill this in) (also can be defined in the 
        ///          initialization file, if used)
        /// </summary>
        public int Height
        {
            set
            {
                int MonitorWidth, MonitorHeight;

                GetMonitorDimensions(out MonitorWidth, out MonitorHeight);
                if ((value < 40) || (value > MonitorHeight))
                    ShowError(HEIGHT_RANGE_ERROR, value);
                else
                {
                    if (!_disposed)
                    {
                        int Ret = 0, Width;

                        Width = ScopeGetFormWidth(scopeHandle);
                        Ret = ScopeSetFormSize(scopeHandle, Width, value);
                        if (Ret != scopeHandle)
                            ShowError(SCOPE_HANDLE_ERROR);
                    }
                    else
                        ShowError(SCOPE_DISPOSED_ERROR);
                }
            }

            get
            {
                if (!_disposed)
                    return ScopeGetFormHeight(scopeHandle);
                else
                    ShowError(SCOPE_HANDLE_ERROR);
                return 0;
            }
        }

        /// <summary>
        /// Property: CellSize
        ///
        /// Sets the size of a grid cell in pixels; sets both height
        /// and width of cells to the single parameter setting.
        ///
        /// Value:   int, Range: 10-150
        /// Access:  Read/Write
        /// Default: 40 (also can be defined in the initialization file, if used)
        /// </summary>
        public int CellSize
        {
            set
            {
                if ((value < 10) || (value > 150))
                    ShowError(CELLSIZE_RANGE_ERROR, value);
                else
                {
                    if (!_disposed)
                    {
                        int Ret = ScopeSetCellPixelSize(scopeHandle, value);
                        if (Ret != scopeHandle)
                            ShowError(SCOPE_HANDLE_ERROR);
                    }
                    else
                        ShowError(SCOPE_DISPOSED_ERROR);
                }
            }

            get
            {
                if (!_disposed)
                    return ScopeGetCellPixelSize(scopeHandle);
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return 0;
            }
        }

        /// <summary>
        /// Property: SamplesPerGridCell
        /// 
        /// Sets the number of samples per grid cell on the horizontal (time) axis.
        /// The value will be adjusted to closest valid number (see the DLL 
        /// documentation).
        /// 
        /// WARNING: if this setting is too low and the data stream is too fast
        /// a buffer overrun can be caused, which will have no indicator other
        /// than the DLL locking up.
        /// 
        /// Value:   double, Range 0.10000-20,000
        /// Access:  Read/Write
        /// Default: (->fill this in) (also can be defined in the 
        ///          initialization file, if used)
        /// </summary>
        public double SamplesPerGridCell
        {
            set
            {
                if ((value < 0.10000) || (value > 20000.00000))
                    ShowError(SAMPLESPERGRIDCELL_RANGE_ERROR, value);
                else
                {
                    if (!_disposed)
                    {
                        int Ret = ScopeSetCellSampleSize(scopeHandle, value);
                        if (Ret != scopeHandle)
                            ShowError(SCOPE_HANDLE_ERROR);
                    }
                    else
                        ShowError(SCOPE_DISPOSED_ERROR);
                }
            }

            get
            {
                if (!_disposed)
                    return ScopeGetCellSampleSize(scopeHandle);
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return 0.00000;
            }
        }

        /// <summary>
        /// Property: Caption
        /// 
        /// Sets the caption at the top of the oscilloscope form.
        /// 
        /// Value:   string, Range 0-250 characters
        /// Access:  Write Only
        /// Default: "Oscilloscope" (also can be defined in the 
        ///          initialization file, if used)
        /// </summary>
        public string Caption
        {
            set
            {
                if (value.Length > 250)
                    ShowError(CAPTIONSIZE_ERROR);
                else
                {
                    if (!_disposed)
                    {
                        int Ret = ScopeSetCaption(scopeHandle, new StringBuilder(value));
                        if (Ret != scopeHandle)
                            ShowError(SCOPE_HANDLE_ERROR);
                    }
                    else
                        ShowError(SCOPE_DISPOSED_ERROR);
                }
            }
        }

        /// <summary>
        /// Property: AmplitudeScale1
        /// 
        /// Sets the Amplitude Scale of Trace 1's grid divisions, in binary steps/grid cell
        /// The value will be adjusted to closest valid number (see the DLL documentation)
        /// 
        /// Value:   double, Range: 0.00001-100,000.00000
        /// Access:  Read/Write
        /// Default: (->fill this in) (also can be defined in the 
        ///          initialization file, if used)
        /// </summary>
        public double AmplitudeScale1
        {
            set
            {
                if ((value < 0.00001) || (value > 100000.00000))
                    ShowError(CELLAMPLITUDESCALE_RANGE_ERROR, value);
                else
                {
                    if (!_disposed)
                    {
                        int Ret = ScopeSetAmpScale(scopeHandle, 0, value);
                        if (Ret != scopeHandle)
                            ShowError(SCOPE_HANDLE_ERROR);
                    }
                    else
                        ShowError(SCOPE_DISPOSED_ERROR);
                }
            }

            get
            {
                if (!_disposed)
                    return ScopeGetAmpScale(scopeHandle, 0);
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return 0.00000;
            }
        }

        /// <summary>
        /// Property: AmplitudeScale2
        /// 
        /// Sets the Amplitude Scale Trace 2's grid divisions, in binary steps/grid cell
        /// The value will be adjusted to closest valid number (see the DLL documentation)
        /// 
        /// Value:   double, Range: 0.00001-100,000.00000
        /// Access:  Read/Write
        /// Default: (->fill this in) (also can be defined in the 
        ///          initialization file, if used)
        /// </summary>
        public double AmplitudeScale2
        {
            set
            {
                if ((value < 0.00001) || (value > 100000.00000))
                    ShowError(CELLAMPLITUDESCALE_RANGE_ERROR, value);
                else
                {
                    if (!_disposed)
                    {
                        int Ret = ScopeSetAmpScale(scopeHandle, 1, value);
                        if (Ret != scopeHandle)
                            ShowError(SCOPE_HANDLE_ERROR);
                    }
                    else
                        ShowError(SCOPE_DISPOSED_ERROR);
                }
            }

            get
            {
                if (!_disposed)
                    return ScopeGetAmpScale(scopeHandle, 1);
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return 0.00000;
            }
        }

        /// <summary>
        /// Property: AmplitudeScale3
        /// 
        /// Sets the Amplitude Scale of Trace 3's grid divisions, in binary steps/grid cell
        /// The value will be adjusted to closest valid number (see the DLL documentation)
        /// 
        /// Value:   double, Range: 0.00001-100,000.00000
        /// Access:  Read/Write
        /// Default: (->fill this in) (also can be defined in the 
        ///          initialization file, if used)
        /// </summary>
        public double AmplitudeScale3
        {
            set
            {
                if ((value < 0.00001) || (value > 100000.00000))
                    ShowError(CELLAMPLITUDESCALE_RANGE_ERROR, 0);
                else
                {
                    if (!_disposed)
                    {
                        int Ret = ScopeSetAmpScale(scopeHandle, 2, value);
                        if (Ret != scopeHandle)
                            ShowError(SCOPE_HANDLE_ERROR);
                    }
                    else
                        ShowError(SCOPE_DISPOSED_ERROR);
                }
            }

            get
            {
                if (!_disposed)
                    return ScopeGetAmpScale(scopeHandle, 2);
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return 0.00000;
            }
        }

        /// <summary>
        /// Property: VerticalOffset1
        /// 
        /// Sets the Vertical Offset of Trace 1, in units of of Trace 1's
        /// AmplitudeScale.
        /// 
        /// Value:   double, Range: -1,000,000.00000-+1,000,000.00000
        /// Access:  Read/Write
        /// Default: 0.00000 (also can be defined in the initialization file, if used)
        /// </summary>
        public double VerticalOffset1
        {
            set
            {
                if ((value < -1000000.0000) || (value > 1000000.00000))
                    ShowError(VERTICALOFFSET_RANGE_ERROR, value);
                else
                {
                    if (!_disposed)
                    {
                        int Ret = ScopeSetAmpOffset(scopeHandle, 0, value);
                        if (Ret != scopeHandle)
                            ShowError(SCOPE_HANDLE_ERROR);
                    }
                    else
                        ShowError(SCOPE_DISPOSED_ERROR);
                }
            }

            get
            {
                if (!_disposed)
                    return ScopeGetAmpOffset(scopeHandle, 0);
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return 0.00000;
            }
        }

        /// <summary>
        /// Property: VerticalOffset2
        /// 
        /// Sets the Vertical Offset of Trace 2, in units of of Trace 2's
        /// AmplitudeScale.
        /// 
        /// Value:   double, Range: -1,000,000.00000-+1,000,000.00000
        /// Access:  Read/Write
        /// Default: 0.00000 (also can be defined in the initialization file, if used)
        /// </summary>
        public double VerticalOffset2
        {
            set
            {
                if ((value < -1000000.0000) || (value > 1000000.00000))
                    ShowError(VERTICALOFFSET_RANGE_ERROR, value);
                else
                {
                    if (!_disposed)
                    {
                        int Ret = ScopeSetAmpOffset(scopeHandle, 1, value);
                        if (Ret != scopeHandle)
                            ShowError(SCOPE_HANDLE_ERROR);
                    }
                    else
                        ShowError(SCOPE_DISPOSED_ERROR);
                }
            }

            get
            {
                if (!_disposed)
                    return ScopeGetAmpOffset(scopeHandle, 1);
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return 0.00000;
            }
        }

        /// <summary>
        /// Property: VerticalOffset3
        /// 
        /// Sets the Vertical Offset of Trace 3, in units of of Trace 3's
        /// AmplitudeScale.
        /// 
        /// Value:   double, Range: -1,000,000.00000-+1,000,000.00000
        /// Access:  Read/Write
        /// Default: 0.00000 (also can be defined in the initialization file, if used)
        /// </summary>
        public double VerticalOffset3
        {
            set
            {
                if ((value < -1000000.0000) || (value > 1000000.00000))
                    ShowError(VERTICALOFFSET_RANGE_ERROR, value);
                else
                {
                    if (!_disposed)
                    {
                        int Ret = ScopeSetAmpOffset(scopeHandle, 2, value);
                        if (Ret != scopeHandle)
                            ShowError(SCOPE_HANDLE_ERROR);
                    }
                    else
                        ShowError(SCOPE_DISPOSED_ERROR);
                }
            }

            get
            {
                if (!_disposed)
                    return ScopeGetAmpOffset(scopeHandle, 2);
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return 0.00000;
            }
        }

        /// <summary>
        /// Property: TriggerLevel1
        /// 
        /// Sets the Trigger Offset Trace 1, in units of Trace 1's AmplitudeScale.
        /// 
        /// Value:   double, Range: -1,000,000.00000-+1,000,000.00000
        /// Access:  Read/Write
        /// Default: 0.00000 (also can be defined in the initialization file, if used)
        /// </summary>
        public double TriggerLevel1
        {
            set
            {
                if ((value < -1000000.0000) || (value > 1000000.00000))
                    ShowError(TRIGGERLEVEL_RANGE_ERROR, value);
                else
                {
                    if (!_disposed)
                    {
                        int Ret = ScopeSetTriggerLevel(scopeHandle, 0, value);
                        if (Ret != scopeHandle)
                            ShowError(SCOPE_HANDLE_ERROR);
                    }
                    else
                        ShowError(SCOPE_DISPOSED_ERROR);
                }
            }

            get
            {
                if (!_disposed)
                    return ScopeGetTriggerLevel(scopeHandle, 0);
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return 0.00000;
            }
        }

        /// <summary>
        /// Property: TriggerLevel2
        /// 
        /// Sets the Trigger Offset Trace 2, in units of Trace 2's AmplitudeScale.
        /// 
        /// Value:   double, Range: -1,000,000.00000-+1,000,000.00000
        /// Access:  Read/Write
        /// Default: 0.00000 (also can be defined in the initialization file, if used)
        /// </summary>
        public double TriggerLevel2
        {
            set
            {
                if ((value < -1000000.0000) || (value > 1000000.00000))
                    ShowError(TRIGGERLEVEL_RANGE_ERROR, value);
                else
                {
                    if (!_disposed)
                    {
                        int Ret = ScopeSetTriggerLevel(scopeHandle, 1, value);
                        if (Ret != scopeHandle)
                            ShowError(SCOPE_HANDLE_ERROR);
                    }
                    else
                        ShowError(SCOPE_DISPOSED_ERROR);
                }
            }

            get
            {
                if (!_disposed)
                    return ScopeGetTriggerLevel(scopeHandle, 1);
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return 0.00000;
            }
        }

        /// <summary>
        /// Property: TriggerLevel3
        /// 
        /// Sets the Trigger Offset Trace 3, in units of Trace 3's AmplitudeScale.
        /// 
        /// Value:   double, Range: -1,000,000.00000-+1,000,000.00000
        /// Access:  Read/Write
        /// Default: 0.00000 (also can be defined in the initialization file, if used)
        /// </summary>
        public double TriggerLevel3
        {
            set
            {
                if ((value < -1000000.0000) || (value > 1000000.00000))
                    ShowError(TRIGGERLEVEL_RANGE_ERROR, value);
                else
                {
                    if (!_disposed)
                    {
                        int Ret = ScopeSetTriggerLevel(scopeHandle, 2, value);
                        if (Ret != scopeHandle)
                            ShowError(SCOPE_HANDLE_ERROR);
                    }
                    else
                        ShowError(SCOPE_DISPOSED_ERROR);
                }
            }

            get
            {
                if (!_disposed)
                    return ScopeGetTriggerLevel(scopeHandle, 2);
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return 0.00000;
            }
        }

        /// <summary>
        /// Property: TriggerSource
        /// 
        /// Sets the Oscilloscope's Trigger Source. The Constants TRACE1_TRIG,
        /// TRACE2_TRIG, TRACE3_TRIG and EXTERNAL_TRIG are provided for conenience.
        /// 
        /// Value:   int, Range 0-3 (TRACE1_TRIG-EXTERNAL_TRIG)
        /// Access:  Read/Write
        /// Default  TRACE1_TRIG (also can be defined in the initialization file, if used)
        /// </summary>
        public int TriggerSource
        {
            set
            {
                if (!_Scope_Visible)
                    ShowError(SCOPE_NOT_VISIBLE);
                else if ((value < TRACE1_TRIG) || (value > EXTERNAL_TRIG))
                    ShowError(TRIGGERSOURCE_RANGE_ERROR, value);
                else
                {
                    if (!_disposed)
                    {
                        try
                        {
                            int Ret = ScopeSetTriggerSourse(scopeHandle, value);
                            if (Ret != scopeHandle)
                                ShowError(SCOPE_HANDLE_ERROR);
                        }
                        catch
                        {
                            ShowError(SCOPE_TRIGGERSOURCE_ERROR);
                            Dispose();
                        }
                    }
                    else
                        ShowError(SCOPE_DISPOSED_ERROR);
                }
            }

            get
            {
                if (!_disposed)
                    return ScopeGetTriggerSourse(scopeHandle);
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return 0;
            }
        }

        /// <summary>
        /// Property: TriggerEdge
        /// 
        /// Sets the Trigger Edge Detection Mode. Any valid negative int sets it 
        /// Negative Edge Triggered, any valid positive int sets it Positive Edge 
        /// Triggered, and 0 set it to disabled. The constants FALLING_EDGE_TRIG,
        /// RISING_EDGE_TRIG and DISABLED_TRIG are provided for convenience.
        /// 
        /// Value:   int, Range: see above
        /// Access;  Read/Write
        /// Default: DISABLED_TRIG (also can be defined in the initialization file, 
        ///          if used)
        /// </summary>
        public int TriggerEdge
        {
            set
            {
                if (!_Scope_Visible)
                    ShowError(SCOPE_NOT_VISIBLE);
                else if (!_disposed)
                {
                    int Edge;

                    try
                    {
                        if (value < 0)
                            Edge = -1;
                        else if (value > 0)
                            Edge = 1;
                        else
                            Edge = 0;
                        int Ret = ScopeSetActiveTriggerEdge(scopeHandle, Edge);
                        if (Ret != scopeHandle)
                            ShowError(SCOPE_HANDLE_ERROR);
                    }
                    catch
                    {
                        ShowError(SCOPE_TRIGGEREDGE_ERROR);
                        Dispose();
                    }
                }
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
            }

            get
            {
                if (!_disposed)
                    return ScopeGetActiveTriggerEdge(scopeHandle);
                else
                    ShowError(SCOPE_HANDLE_ERROR);
                return 0;
            }
        }

        /// <summary>
        /// Property: TriggerDelay
        /// 
        /// Controls the Trigger Delay, in number of samples.
        /// (see DLL Documentation)
        /// 
        /// Value:   int, RangeL 0-Any valid positive int value
        /// Access:  Read/Write
        /// Default: 0 (also can be defined in the initialization file, if used)
        /// </summary>
        public int TriggerDelay
        {
            set
            {
                if (value < 0)
                    ShowError(TRIGGERDELAY_RANGE_ERROR, value);
                else
                {
                    if (!_disposed)
                    {
                        int Ret = ScopeSetTrigHoldOffSmplsNum(scopeHandle, value);
                        if (Ret != scopeHandle)
                            ShowError(SCOPE_HANDLE_ERROR);
                    }
                    else
                        ShowError(SCOPE_DISPOSED_ERROR);
                }
            }

            get
            {
                if (!_disposed)
                    return ScopeGetTrigHoldOffSmplsNum(scopeHandle);
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return 0;
            }
        }

        /// <summary>
        /// Property: TraceDisplayWidth
        /// 
        /// Returns the width of the current trace display area display in pixels.
        /// 
        /// Value:   int
        /// Access:  Read Only
        /// Default: N/A
        /// </summary>
        public int TraceDisplayWidth
        {
            get
            {
                if (!_disposed)
                    return (ScopeGetScreenWidth(scopeHandle));
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return 0;
            }
        }

        /// <summary>
        /// Property: TraceDisplayHeight
        /// 
        /// Returns the height of the current trace display area in pixels.
        /// 
        /// Value:   int
        /// Access:  Read Only
        /// Default: N/A
        /// </summary>
        public int TraceDisplayHeight
        {
            get
            {
                if (!_disposed)
                    return (ScopeGetScreenHeight(scopeHandle));
                else
                    ShowError(SCOPE_DISPOSED_ERROR);
                return 0;
            }
        }

        /// <summary>
        /// Method to Display Error Messages, overload for errors that have no associated
        /// data (fundamental DLL communication errors, except for CAPTIONSIZE_ERROR). 
        /// </summary>
        /// <param name="MessageNum">int: Error Number</param>
        private static void ShowError(int ErrorNum)
        {
            _LastError = ErrorNum;
            if (_ErrorMessagesOn)
            {
                switch (ErrorNum)
                {
                    case SCOPE_CREATION_ERROR:
                        throw new Exception("Oscilloscope Creation Failed.");
                    case SCOPE_HANDLE_ERROR:
                        throw new Exception("Oscilloscope Access Failed.");
                    case SCOPE_DISPOSED_ERROR:
                        throw new Exception("Oscilloscope has been Disposed.");
                    case CAPTIONSIZE_ERROR:
                        throw new Exception("Caption Too Long.");
                    case SCOPE_TRIGGERSOURCE_ERROR:
                        throw new Exception("The Oscilloscope threw a Set Trigger Source Exception and must be Closed.");
                    case SCOPE_TRIGGEREDGE_ERROR:
                        throw new Exception("The Oscilloscope threw a Set Trigger Edge Exception and must be Closed.");
                    case SCOPE_NOT_VISIBLE:
                        throw new Exception("This Property Cannot be Accessed when the Oscilloscope is in Hide Mode.");
                }
            }
        }

        /// <summary>
        /// Method to Display Error Messages, overload for range errors in property 
        /// that are type integer.
        /// </summary>
        /// <param name="MessageNum">int: Error Number</param>
        /// <param name="ErrorValue">int: The Value That Caused the Error</param>
        private static void ShowError(int ErrorNum, int ErrorValue)
        {
            _LastError = ErrorNum;
            if (_ErrorMessagesOn)
            {
                switch (ErrorNum)
                {
                    case LEFT_RANGE_ERROR:
                        throw new Exception(string.Format("Scope Left Out of Range: {0}.", ErrorValue));
                    case TOP_RANGE_ERROR:
                        throw new Exception(string.Format("Scope Top Out Of Range: {0}.", ErrorValue));
                    case WIDTH_RANGE_ERROR:
                        throw new Exception(string.Format("Scope Width Out of Range: {0}", ErrorValue));
                    case HEIGHT_RANGE_ERROR:
                        throw new Exception(string.Format("Scope Height Out Of Range: {0}.", ErrorValue));
                    case CELLSIZE_RANGE_ERROR:
                        throw new Exception(string.Format("Scope CellSize Out of Range: {0}.", ErrorValue));
                    case TRIGGERSOURCE_RANGE_ERROR:
                        throw new Exception(string.Format("Scope Trigger Source Out of Range: {0}.", ErrorValue));
                    case TRIGGERDELAY_RANGE_ERROR:
                        throw new Exception(string.Format("Scope Trigger Delay Out of Range: {0}.", ErrorValue));

                }
            }
        }

        /// <summary>
        /// Method to Display Error Messages, overload for range errors in properties 
        /// that are type double.
        /// </summary>
        /// <param name="MessageNum">int: Error Number</param>
        /// <param name="ErrorValue">double: The Value That Caused the Error</param>
        private static void ShowError(int ErrorNum, double ErrorValue)
        {
            _LastError = ErrorNum;
            if (_ErrorMessagesOn)
            {
                switch (ErrorNum)
                {
                    case SAMPLESPERGRIDCELL_RANGE_ERROR:
                        throw new Exception(string.Format("Scope SamplesPerGridCell Out of Range: {0}", ErrorValue));
                    case CELLAMPLITUDESCALE_RANGE_ERROR:
                        throw new Exception(string.Format("Scope CellAmplitideScale Out of Range: {0}", ErrorValue));
                    case VERTICALOFFSET_RANGE_ERROR:
                        throw new Exception(string.Format("Vertical Offset Out of Range: {0}", ErrorValue));
                    case TRIGGERLEVEL_RANGE_ERROR:
                        throw new Exception(string.Format("Trigger Level Out of Range: {0}", ErrorValue));
                }
            }
        }

        public static void GetMonitorDimensions(out int Width, out int Height)
        {
            Size Monitor = SystemInformation.PrimaryMonitorSize;
            Width = Monitor.Width;
            Height = Monitor.Height;
        }

        #region IDisposable Members

        /// <summary>
        /// Dispose the object - call this to release the oscilloscope resources.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                ScopeDestroy(scopeHandle);
                _disposed = true;
            }
        }

        /// <summary>
        /// Flag indicating if the object is already disposed.
        /// /// </summary>
        public bool IsDisposed
        {
            get
            {
                return _disposed;
            }
        }

        #endregion
    }
}