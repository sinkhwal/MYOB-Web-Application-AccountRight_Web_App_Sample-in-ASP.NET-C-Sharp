////  Copyright:   Copyright 2013 MYOB Technology Pty Ltd. All rights reserved.
////  Website:     http://www.myob.com
////  Author:      MYOB
////  E-mail:      info@myob.com
////
//// Documentation, code and sample applications provided by MYOB Australia are for 
//// information purposes only. MYOB Technology Pty Ltd and its suppliers make no 
//// warranties, either express or implied, in this document. 
////
//// Information in this document or code, including website references, is subject
//// to change without notice. Unless otherwise noted, the example companies, 
//// organisations, products, domain names, email addresses, people, places, and 
//// events are fictitious. 
////
//// The entire risk of the use of this documentation or code remains with the user. 
//// Complying with all applicable copyright laws is the responsibility of the user. 
////
//// Copyright 2013 MYOB Technology Pty Ltd. All rights reserved.

//using System.Drawing;
//using System.Windows.Forms;
//using MYOB.AccountRight.SDK.Contracts.Version2.Contact;

//namespace Web.Helpers
//{
//    /// <summary>
//    /// Custom DataGridView Column for displaying a cardlink
//    /// </summary>
//    /// <remarks></remarks>
//    public sealed class DataGridViewCardLinkColumn : DataGridViewColumn
//    {
//        public DataGridViewCardLinkColumn()
//        {
//            CellTemplate = new DataGridViewCardLinkCell();
//            ReadOnly = true;
//        }
//    }

//    /// <summary>
//    /// Custom DataGridView Cell for displaying a cardlink
//    /// </summary>
//    /// <remarks></remarks>
//    public class DataGridViewCardLinkCell : DataGridViewTextBoxCell
//    {
//        protected override void Paint(Graphics graphics, Rectangle clipBounds,
//                                      Rectangle cellBounds, int rowIndex,
//                                      DataGridViewElementStates cellState, object value, object formattedValue,
//                                      string errorText, DataGridViewCellStyle cellStyle,
//                                      DataGridViewAdvancedBorderStyle advancedBorderStyle,
//                                      DataGridViewPaintParts paintParts)
//        {
//            //The value passed to cell is a CardLink object, set the formatted value to be the Name on the card
//            var card = value as CardLink;
//            if (card != null)
//                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, card, card.Name, errorText, cellStyle,
//                           advancedBorderStyle,
//                           paintParts);
//        }
//    }
//}