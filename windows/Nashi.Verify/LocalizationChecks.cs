using System.Text.RegularExpressions;
using Nashi.Core;
internal static class LocalizationChecks
{
    internal static void Run(Action<bool,string> check,string root)
    {
        foreach(var source in FeatureCatalog.ToolNames.Values.Concat(Enum.GetValues<WindowPlacement>().Select(p=>p.Title())))check(AppLocalization.Text(source,"tr")!=source,"Missing Turkish tool/layout copy: "+source);
        check(AppLocalization.Text("Library","tr")=="Kitaplık"&&AppLocalization.Text("Language","tr")=="Dil","Turkish UI catalog not loaded");
        check(AppLocalization.Text("RESIZE IMAGE","tr")=="GÖRSELİ BOYUTLANDIR","Turkish dotted I was lost in uppercase wheel labels");
        check(AppLocalization.Text("SHOW NASHI IN FINDER","tr")=="NASHI'Yİ FINDER'DA GÖSTER"&&AppLocalization.Uppercase("Shift'e bas","tr")=="SHIFT'E BAS"&&AppLocalization.Uppercase("Shift","en")=="SHIFT","Turkish upper case dotted a Latin product or key name");
        check(AppLocalization.Matches("Crop Image","kırp","tr")&&AppLocalization.Matches("Crop Image","görsel","tr"),"Turkish search did not match localized tool names");
        var raw="Türkçe {0} $1 \\ image.png";
        check(AppLocalization.Text("Original size: "+raw,"tr")=="Orijinal boyut: "+raw,"Interpolation changed a file/user value");
        check(AppLocalization.Text("Nicely done. 12 files are ready.","tr")=="Hazır! 12 dosyan hazır.","Count interpolation failed");
        check(AppLocalization.Text("Couldn’t finish","tr")=="İşlem tamamlanamadı"&&AppLocalization.Text("Choose at least two PDFs.","tr")=="En az iki PDF seç.","Windows dialog titles or error messages stayed in English");
        check(AppLocalization.Text("Download, verify and install Nashi 0.8.0? The app will restart. No account or token is needed.","tr")=="Nashi 0.8.0 indirilip doğrulansın ve kurulsun mu? Uygulama yeniden başlayacak. Hesap veya token gerekmez."&&AppLocalization.Text("Office could not convert this document to PDF. ","tr")=="Office bu belgeyi PDF biçimine dönüştüremedi. ","Windows dialog templates lost their inserted values");
        foreach(var value in new[]{"system","light","dark","glass","solid","gradient","grid","row","column","featured","video","audio","thumbnail","photos","image","waveform","spectrum","bars","bleep","mute","blur","pixelate","matchSource","pcm16","pcm24","pcm32","float32","editable","preserve","reflowable","fixed","createNew","addToExisting","beside","ask","folder","Magnifier","Freehand"})check(AppLocalization.Text(value,"tr")!=value,"Untranslated Windows choice: "+value);
        check(AppLocalization.Text("2 pages","tr")=="2 sayfa","PDF preview page count was not localized");
        check(AppLocalization.Text("Drag to select a crop. Hold Shift for a square; existing selections keep their ratio. Press Enter to save.","tr").StartsWith("Kırpılacak alanı seçmek için sürükle.",StringComparison.Ordinal)&&AppLocalization.Text("Note","tr")=="Not","Image editor instructions/default annotation remained English");
        check(AppLocalization.Text("Update to 1.2.3","tr")=="1.2.3 sürümüne güncelle"&&AppLocalization.Text("photo.png · Preview · Nashi","tr")=="photo.png · Önizleme · Nashi","Dynamic update or filename preview title did not localize");
        check(AppLocalization.Text("Add or change files","tr")=="Dosya ekle veya değiştir"&&AppLocalization.Text("✦  READY WHEN YOU ARE","tr")=="✦  HAZIRSAN BAŞLAYALIM","Selected-file home copy remained English");
        check(AppLocalization.Text("3 photos · full-frame images · saved in post order","tr")=="3 fotoğraf · tam kare görseller · gönderi sırasıyla kaydedilir","Web photo inspection summary was not localized");
        foreach(var value in new[]{"Home.png","MP4","Ctrl+Shift","⌘S","00:01.250"})check(AppLocalization.Text(value,"tr")==value,"Translation changed a format, path or shortcut");
        foreach(var (source,target) in AppLocalization.Turkish)check(Regex.Matches(source,@"\{[0-9]\}").Select(m=>m.Value).Order().SequenceEqual(Regex.Matches(target,@"\{[0-9]\}").Select(m=>m.Value).Order()),"Translation placeholders differ: "+source);
        var originalCulture=System.Globalization.CultureInfo.CurrentUICulture;
        try
        {
            System.Globalization.CultureInfo.CurrentUICulture=System.Globalization.CultureInfo.GetCultureInfo("en-US");
            var directory=Path.Combine(root,"Localization preferences");var store=new PreferencesStore(directory);check(store.Value.Language=="en","English users did not get the English default");
            store.Save(store.Value with{Language="tr",Feedback=false,ToolsModifier="Ctrl+Alt"});var loaded=new PreferencesStore(directory).Value;
            check(loaded.Language=="tr"&&!loaded.Feedback&&loaded.ToolsModifier=="Ctrl+Alt","Language persistence changed other preferences");
            var old=Path.Combine(root,"Legacy language preferences");Directory.CreateDirectory(old);File.WriteAllText(Path.Combine(old,"preferences.json"),"{\"Feedback\":false}");var legacy=new PreferencesStore(old);
            check(legacy.Value.Language=="en"&&!legacy.Value.Feedback,"Legacy preference migration changed other values");
            System.Globalization.CultureInfo.CurrentUICulture=System.Globalization.CultureInfo.GetCultureInfo("tr-TR");var turkish=new PreferencesStore(Path.Combine(root,"Turkish default language"));
            check(turkish.Value.Language=="tr","New Turkish Windows profiles did not default to Turkish");
        }
        finally{System.Globalization.CultureInfo.CurrentUICulture=originalCulture;}
        check(AppLocalization.Normalize("fr")=="en"&&AppLocalization.Text("Home","en")=="Home","Unsupported language or English fallback failed");
    }
}
