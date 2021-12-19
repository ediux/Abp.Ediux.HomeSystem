using AutoMapper;

namespace Ediux.HomeSystem.Models.ValueConverters
{

    public class StringToBooleanConverter : IValueConverter<string, bool>,IValueConverter<bool,string>
    {
        public bool Convert(string sourceMember, ResolutionContext context)
        {
            return bool.Parse(sourceMember);
        }

        public string Convert(bool sourceMember, ResolutionContext context)
        {
            return sourceMember.ToString();
        }
    }
}
