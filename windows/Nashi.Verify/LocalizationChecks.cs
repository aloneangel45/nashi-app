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
        foreach(var (source,target) in new[]{
            ("Packing files…","Dosyalar paketleniyor…"),("Extracting files…","Dosyalar çıkarılıyor…"),("Saving archive…","Arşiv kaydediliyor…"),("Saving your files…","Dosyaların kaydediliyor…"),
            ("Saving document…","Belge kaydediliyor…"),("Downloading ExifTool…","ExifTool indiriliyor…"),("Metadata tools ready","Üst veri araçları hazır"),("Saving metadata…","Üst veriler kaydediliyor…"),
            ("Preparing move…","Taşıma hazırlanıyor…"),("Saving collage…","Kolaj kaydediliyor…"),("Adding pages…","Sayfalar ekleniyor…"),("Saving images…","Görseller kaydediliyor…"),
            ("Media tools ready","Medya araçları hazır"),("Office tools ready","Ofis araçları hazır"),("Preparing office tools…","Ofis araçları hazırlanıyor…"),
            ("Resolving media…","Medya bilgileri alınıyor…"),("Downloading media…","Medya indiriliyor…"),("Saving files…","Dosyalar kaydediliyor…"),("Downloading image…","Görsel indiriliyor…"),
            ("Measuring loudness…","Ses yüksekliği ölçülüyor…"),("Joining clips…","Klipler birleştiriliyor…"),
            ("Play Result","Sonucu Oynat"),("Preview Visualization","Görselleştirmeyi Önizle"),("Preview Processed","İşlenmiş Hâli Önizle"),
            ("Nashi tools","Nashi araçları"),("Nashi Window Management","Nashi pencere yönetimi"),
            ("This file could not be previewed. Use Open to view it in its default app.","Bu dosya önizlenemedi. Varsayılan uygulamasında açmak için Aç'ı kullan."),
            ("Nashi · Update 1.2.3 available","Nashi · 1.2.3 güncellemesi var"),
            ("Downloading LibreOffice 26.8.1…","LibreOffice 26.8.1 indiriliyor…"),("Verifying update signature…","Güncelleme imzası doğrulanıyor…")})
            check(AppLocalization.Text(source,"tr")==target,"Windows progress copy stayed untranslated: "+source);
        foreach(var (source,target) in new[]{
            ("Downloading FFmpeg 9.0…","FFmpeg 9.0 indiriliyor…"),("Downloading yt-dlp…","yt-dlp indiriliyor…"),("Preparing photo.png…","photo.png hazırlanıyor…"),
            ("Rendering page 3","3. sayfa oluşturuluyor"),("Fitting image · attempt 2…","Görsel sığdırılıyor · deneme 2…"),("Photo 2 of 5…","Fotoğraf 2/5…"),
            ("Compressing PDF · attempt 2 · page 3/8…","PDF sıkıştırılıyor · deneme 2 · sayfa 3/8…"),("Downloading Nashi 0.8.0…","Nashi 0.8.0 indiriliyor…"),
            ("Joining clip 2 of 4","Klip 2/4 birleştiriliyor"),("Compression attempt 2…","Sıkıştırma denemesi 2…"),
            ("Compression 2 · analyzing","Sıkıştırma 2 · analiz ediliyor"),("Compression 2 · encoding","Sıkıştırma 2 · kodlanıyor"),
            ("Saving clip 2 of 4","Klip 2/4 kaydediliyor"),("Photo 42 · 1920 × 1080","Fotoğraf 42 · 1920 × 1080"),
            ("Use notes.png again","notes.png öğesini yeniden kullan"),("Show notes.png in Explorer","notes.png öğesini Dosya Gezgini'nde göster"),
            ("Preview notes.png","notes.png öğesini önizle")})
            check(AppLocalization.Text(source,"tr")==target,"Dynamic Windows progress copy stayed untranslated: "+source);
        check(AppLocalization.Text("Download, verify and install Nashi 0.8.0? The app will restart. No account or token is needed.","tr")=="Nashi 0.8.0 indirilip doğrulansın ve kurulsun mu? Uygulama yeniden başlayacak. Hesap veya token gerekmez."&&AppLocalization.Text("Office could not convert this document to PDF. ","tr")=="Office bu belgeyi PDF biçimine dönüştüremedi. ","Windows dialog templates lost their inserted values");
        foreach(var value in new[]{"system","light","dark","glass","solid","gradient","grid","row","column","featured","video","audio","thumbnail","photos","image","waveform","spectrum","bars","bleep","mute","blur","pixelate","matchSource","pcm16","pcm24","pcm32","float32","editable","preserve","reflowable","fixed","createNew","addToExisting","beside","ask","folder","Magnifier","Freehand"})check(AppLocalization.Text(value,"tr")!=value,"Untranslated Windows choice: "+value);
        check(AppLocalization.Text("2 pages","tr")=="2 sayfa","PDF preview page count was not localized");
        check(AppLocalization.Text("Drag to select a crop. Hold Shift for a square; existing selections keep their ratio. Press Enter to save.","tr").StartsWith("Kırpılacak alanı seçmek için sürükle.",StringComparison.Ordinal)&&AppLocalization.Text("Note","tr")=="Not","Image editor instructions/default annotation remained English");
        check(AppLocalization.Text("Update to 1.2.3","tr")=="1.2.3 sürümüne güncelle"&&AppLocalization.Text("photo.png · Preview · Nashi","tr")=="photo.png · Önizleme · Nashi","Dynamic update or filename preview title did not localize");
        check(AppLocalization.Text("Add or change files","tr")=="Dosya ekle veya değiştir"&&AppLocalization.Text("✦  READY WHEN YOU ARE","tr")=="✦  HAZIRSAN BAŞLAYALIM","Selected-file home copy remained English");
        check(AppLocalization.Text("3 photos · full-frame images · saved in post order","tr")=="3 fotoğraf · tam kare görseller · gönderi sırasıyla kaydedilir","Web photo inspection summary was not localized");
        foreach(var (source,target) in new[]{
            ("Choose files for Nashi","Nashi için dosya seç"),("Save downloaded media to…","İndirilen medyanın kaydedileceği yer…"),("Save new files to…","Yeni dosyaların kaydedileceği yer…"),
            ("Save new copies here","Yeni kopyaları buraya kaydet"),("Save downloaded media here","İndirilen medyayı buraya kaydet"),("Keep them together, or give each one a fresh start.","Dosyaları birlikte tut veya her birine yeni bir başlangıç ver."),
            ("Processed preview unavailable for this Windows codec.","Bu Windows codec'i için işlenmiş önizleme kullanılamıyor."),("Preview unavailable. codec error","Önizleme kullanılamıyor. codec error"),
            ("Pasted text. Choose a tool or create a ZIP.","Yapıştırılan metin. Bir araç seç veya ZIP oluştur."),("Pasted image. Choose a tool or create a ZIP.","Yapıştırılan görsel. Bir araç seç veya ZIP oluştur."),
            ("Pasted files. Choose a tool or create a ZIP.","Yapıştırılan dosyalar. Bir araç seç veya ZIP oluştur."),("Shift  Convert","Shift  Dönüştür"),("Ctrl  Tools","Ctrl  Araçlar"),("Alt  File actions","Alt  Dosya işlemleri")})
            check(AppLocalization.Text(source,"tr")==target,"Windows UI copy is untranslated or changed unexpectedly: "+source);
        check(AppLocalization.Text("Preview position","tr")=="Önizleme konumu"&&AppLocalization.Text("Space: preview · Enter: use in Nashi","tr")=="Boşluk: önizle · Enter: Nashi'de kullan","Preview tooltip or Library card hint stayed in English");
        foreach(var (source,target) in new[]{
            ("Download from the web · Nashi","Web'den indir · Nashi"),("Read QR Codes · Nashi","QR Kodlarını Oku · Nashi"),("Metadata · Nashi","Meta Veriler · Nashi"),("Make it yours · Nashi","Sana göre · Nashi"),("Organize PDF · Nashi","PDF'yi düzenle · Nashi"),("Preview · Nashi","Önizleme · Nashi"),
            ("Float · Nashi","Yüzen Pencere · Nashi"),("No QR codes found. Try a larger or sharper image.","QR kodu bulunamadı. Daha büyük veya daha net bir görsel dene."),
            ("1 QR code found.","1 QR kodu bulundu."),("2 QR codes found.","2 QR kodu bulundu."),
            ("Original image · ready to download","Orijinal görsel · indirilmeye hazır"),("YouTube thumbnail · best available full-size image","YouTube küçük resmi · mevcut en yüksek çözünürlük"),
            ("Couldn’t paste: clipboard error","Yapıştırılamadı: clipboard error"),("Edit fields or remove metadata. Encoded audio and video stay untouched.","Alanları düzenle veya üst verileri kaldır. Ses ve video akışları yeniden kodlanmaz."),
            ("Edit fields or remove metadata. Encoded image pixels stay untouched.","Alanları düzenle veya üst verileri kaldır. Görsel pikselleri değiştirilmez."),
            ("Balanced","Dengeli"),("Strong","Güçlü"),("Original dimensions","Orijinal boyutlar"),("Custom dimensions","Özel boyutlar"),("2560 px longest edge","Uzun kenar 2560 px"),("1920 px longest edge","Uzun kenar 1920 px"),("1280 px longest edge","Uzun kenar 1280 px"),("Best available","Mevcut en iyi kalite"),("Up to 1080p","En fazla 1080p"),("Up to 720p","En fazla 720p")})
            check(AppLocalization.Text(source,"tr")==target,"Windows title or dropdown value stayed untranslated: "+source);
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
