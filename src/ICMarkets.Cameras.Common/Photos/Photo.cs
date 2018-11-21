namespace ICMarkets.Cameras.Common.Photos
{
    /// <summary>
    /// Camera photo
    /// </summary>
    /// <remarks>
    /// Camera photo, for example it can be night photo / panorama / smth else.
    /// For our scenario it's not important, so just photo.
    /// </remarks>
    public class Photo : IPhoto
    {
        public override string ToString()
        {
            return @"    ________
   /_______/\
   \ \    / /
 ___\ \__/_/___
/____\ \______/\
\ \   \/ /   / /
 \ \  / /\  / /
  \ \/ /\ \/ /
   \_\/  \_\/";
        }
    }
}