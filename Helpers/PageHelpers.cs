namespace Agility.NET5.Core.Helpers
{
    public static class PageHelpers
    {
        public static string GetPageTemplatePath(string templateName)
        {
            return $"/Views/PageTemplates/{templateName.Replace(" ", string.Empty)}.cshtml";
        }
    }
}
