using System.Web;
using System.Web.Optimization;

namespace rpg
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modaljquery").Include(
                        "~/Scripts/bmodal.js"));

            bundles.Add(new ScriptBundle("~/bundles/Basejquery").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery-2.1.3.min.js",
                        "~/Scripts/Uteis.js"));

            bundles.Add(new StyleBundle("~/bundles/Logincss").Include(
                      "~/Content/Login.css"));

            bundles.Add(new StyleBundle("~/bundles/Basecss").Include(
                      "~/Content/Basico.css"));

            bundles.Add(new StyleBundle("~/bundles/Personagemcss").Include(
                      "~/Content/Personagem.css"));

            bundles.Add(new StyleBundle("~/bundles/Mestrecss").Include(
                      "~/Content/Mestre.css"));

            bundles.Add(new StyleBundle("~/bundles/gridcss").Include(
                     "~/Content/Grid.css"));

            bundles.Add(new StyleBundle("~/bundles/formcss").Include(
                     "~/Content/Form.css"));

            bundles.Add(new StyleBundle("~/bundles/modalcss").Include(
                     "~/Content/Modal.css"));

            bundles.Add(new ScriptBundle("~/bundles/UIjquery").Include(
                        "~/Scripts/jquery-ui - 1.11.3.min.js"));

            bundles.Add(new StyleBundle("~/bundles/UI_css").Include(
                      "~/Content/jquery_ui.min.css",
                      "~/Content/jquery_ui.theme.min.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}
