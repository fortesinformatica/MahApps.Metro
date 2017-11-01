#pragma warning disable 618
namespace MahApps.Metro.Controls
{
    using System;
    using System.Windows;
    using ControlzEx.Standard;

    public static class WinApiHelper
    {
        /// <summary>
        /// Gets the relative mouse position to the given handle in client coordinates.
        /// </summary>
        /// <param name="hWnd">The handle for this method.</param>
        public static System.Windows.Point GetRelativeMousePosition(IntPtr hWnd)
        {
            try
            {
                if (hWnd == IntPtr.Zero)
                {
                    return new System.Windows.Point();
                }
                var point = WinApiHelper.GetPhysicalCursorPos();
                NativeMethods.ScreenToClient(hWnd, ref point);
                return new System.Windows.Point(point.X, point.Y);
            }
            catch
            {
                return new Point();
            }
        }

        /// <summary>
        /// Try to get the relative mouse position to the given handle in client coordinates.
        /// </summary>
        /// <param name="hWnd">The handle for this method.</param>
        public static bool TryGetRelativeMousePosition(IntPtr hWnd, out System.Windows.Point point)
        {

            try
            {
                point = new Point
                {
                    X = System.Windows.Forms.Cursor.Position.X,
                    Y = System.Windows.Forms.Cursor.Position.Y
                };
                return true;
            }
            catch
            {
                point = new Point();
                return false;
            }
        }

        internal static POINT GetPhysicalCursorPos()
        {
            try
            {
                // Sometimes Win32 will fail this call, such as if you are
                // not running in the interactive desktop. For example,
                // a secure screen saver may be running.
                return NativeMethods.GetPhysicalCursorPos();
            }
            catch (Exception exception)
            {
                return new POINT();
                //throw new MahAppsException("Uups, this should not happen! Sorry for this exception! Is this maybe happend on a virtual machine or via remote desktop? Please let us know, thx.", exception);
            }
        }
    }
}