/*document.addEventListener(
    "DOMContentLoaded", function () {
        alert("Siteye Hoş Geldiniz !");
    }
) */
function testonay(event) {

    if (!event) return;



    if (!document.getElementById("onayKutusu3").checked) {
        event.preventDefault();
        alert("Lütfen bilgilerin doğruluğunu onaylayınız!");
        return;
        

    }
    if (!document.getElementById("onayKutusu2").checked) {
        event.preventDefault();
        alert("Kişisel verilerin korunması bildirimini onaylayınız!");
        return;
        
        
    }
    if (!document.getElementById("onayKutusu1").checked) {
        event.preventDefault();
        alert("Açık rıza metni bildirimini onaylayınız!");
        return;
        
        
    }
   
     array1();

}


function bilgiGoster(id) {
    let bilgi;
    let baslik;
    
    if (id === 'rizaMetniBilgi') {
        baslik = "Rıza Metni";
        bilgi = "Yasal temsilcisi olan çocuğuma ait kişisel verilerin, 'Kişisel Verilerin İşlenme Amacı, Dayanağı ve İlgili Haklarınız' isimli aydınlatma metni çerçevesinde Antalya Büyükşehir Belediyesi tarafından işlenmesine izin veriyorum.";
    } else if (id === 'kisiselVerilerBilgi') {
        baslik = "Kişisel Verilerin İşlenmesi";
        bilgi = "Halk Mama Hizmetine ilişkin Aydınlatma Metni'ni okudum ve bu kapsamda kişisel verilerimin bu hizmet dahilinde ve Antalya Büyükşehir Belediyesi'nin (Belediye) diğer hizmetlerinde yararlanılması ve söz konusu hizmetlerin sağlanması, süreçlerinin planlanması, icrası, iyileştirilmesi ve geliştirilmesine yönelik faaliyetlerin gerçekleştirilmesi amaçlarıyla 6698 sayılı Kişisel Verilerin Korunması Hakkındaki Kanun'a (\"KVKK\") uygun olarak toplanmasına, işlenmesine, güncellenmesine, periyodik olarak kontrol edilmesine, veri tabanında tutulmasına, saklanmasına, gerektiği halde ilgili kamu kurum ve kuruluşları ile iş ilişkisi gereği olarak çözüm ortağı üçüncü kişilerle paylaşılmasını kabul ediyorum.";
    } else if (id === 'aydinlatmaMetniBilgi') {
        baslik = "Aydınlatma Metni";
        bilgi = "Sosyal Hizmetler Dairesi Başkanlığında görevli Meslek Elemanları tarafından bilgileriniz incelendikten sonra uygun görüldüğü takdirde Halk Mama hizmetinden faydalandırılacaksınız. Sadece 6-24 ay arası çocuğu olan ailelerin başvuruları değerlendirmeye alınacaktır.";
    }

    Swal.fire({
        title: baslik,
        text: bilgi,
        icon: 'info',
        showCancelButton: true,
        showCloseButton: true,
        confirmButtonText: 'Onaylıyorum',
        cancelButtonText: 'İptal',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33'

    }).then((result) => {
        if(result.isConfirmed) {
            Swal.fire(
                'Onaylandı',
                'Seçiminiz Kaydedildi.',
                'success'
            );

        } else if (result.dismiss || Swal.DismissReason.cancel) {
            Swal.fire(
                'İptal Edildi',           
                'İşlem iptal edildi.',    
                'error' 
            );


            document.getElementById('onayKutusu1').checked = false;
            if (id === 'rizaMetniBilgi') {
            } else if (id === 'kisiselVerilerBilgi') {
                document.getElementById('onayKutusu2').checked = false;
            } else if (id === 'aydinlatmaMetniBilgi') {
                document.getElementById('onayKutusu3').checked = false;
            }
        }

    });
}

     function formKontrol (event) {
        event.preventDefault();
       

    let errorMessage = [];

    //isim-soyisim koşul

    let isim = document. getElementById('isim').value;
    if (isim === "") {
        errorMessage.push("İsim alanı boş bırakılamaz.");
    }
    let soyisim = document.getElementById('soyisim').value;
    if (soyisim === "") {
        errorMessage.push("Soyisim alanı boş bırakılamaz.");
    } 
 
    
    /*tc algoritma */


    var resultFalse = document.getElementById('resultFalse');

    let tc = document.getElementById('tc').value.trim();
    if (tc === "") {
        errorMessage.push("TC Kimlik No alanı boş bırakılamaz.");

    } else if (!/[0-9]+$/.test(tc)) {
        errorMessage.push("TC Kimlik numarası sadece rakamlardan oluşmalıdır!");
        document.getElementById("tc").value =tc.replace(/[^0-9]/g,'');

    } else if (tc[0] ==="0") {
        errorMessage.push("TC Kimlik numarası '0' ile başlayamaz");

    } else if (tc.length !== 11) {
        errorMessage.push("TC Kimlik numarası 11 haneli olmalıdır.");

    } else {
        // TC numarasının 10. ve 11. hanesini kontrol etme
    
        // Tek haneler (1, 3, 5, 7, 9) toplamı
        let rakamlar = tc.split("").map(Number);
        let oddSum = rakamlar[0] + rakamlar[2] + rakamlar[4] + rakamlar[6] + rakamlar[8]; // Tek haneler toplamı
        let evenSum = rakamlar[1] + rakamlar[3] + rakamlar[5] + rakamlar[7]; // Çift haneler toplamı (9. haneyi dahil etmiyoruz)
    
        // 10. hane doğrulaması: (Tek haneler * 7 - Çift haneler) % 10 = 10. hane
        let check10 = (oddSum * 7 - evenSum) % 10;
    
        // İlk 10 haneli sayının toplamının 10'a bölümünden kalan, 11. hane olmalıdır
        let first10Sum = rakamlar.slice(0, 10).reduce((sum, num) => sum + num, 0);
    
        // Eğer her iki koşul da sağlanmazsa, ortak bir hata mesajı gösterelim
        if (check10 !== rakamlar[9] || first10Sum % 10 !== rakamlar[10]) {
            errorMessage.push("Lütfen geçerli bir TC Kimlik numarası giriniz.");
        }
    }
    
  // telno koşul
    
    let telno =document.getElementById('telno').value;
    if(telno === "") {
        errorMessage.push("Telefon Numarası alanı boş bırakılamaz.");
    } else if(!/[0-9]+$/.test(telno)) {
        errorMessage.push("Telefon numarası sadece rakamlardan oluşmalıdır!");
        document.getElementById("telno").value =telno.replace(/[^0-9]/g,'')

    }

    //dtarih koşul

    let dtarih = document.getElementById ('dtarih').value;
    if(dtarih ==="") {
        errorMessage.push ("Doğum Tarihi alanı boş bırakılamaz.");

    } else {
        let tarih = new Date (dtarih);
        if (tarih.getFullYear() < 1800) {
            errorMessage.push("Lütfen Geçerli Bir Doğum Tarihi Giriniz!")
        }
    }

    if (errorMessage.length > 0) {
        alert(errorMessage.join('\n'));
        return;
    }


//  API'ye gönderme kısmı
    const formData = {
        isim: isim,
        soyisim: soyisim,
        telno: telno,
        dtarih: dtarih,
        tc: tc,
        ilce: document.getElementById("ilce").value,
        mahalle: document.getElementById("mahalle").value
    };

    
    fetch("http://10.20.34.56:80/api", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(formData)
    })
    .then(response => response.json())
    .then(data => {
        console.log("Başarıyla gönderildi:", data);
        Swal.fire("Başarılı!", "Başvurunuz alındı.", "success");
        document.getElementById('basvuruFormu').reset(); // Form başarıyla gönderildikten sonra formu temizle
    })
    .catch(error => {
        console.error("Hata oluştu:", error);
        Swal.fire("Hata!", "Başvuru gönderilemedi.", "error");
    });
};


//veri çekme

document.addEventListener("DOMContentLoaded", function() {
    document.getElementById("veriGetir").addEventListener("click", function() {
        // JSONPlaceholder'dan veri çek
       fetch("https://jsonplaceholder.typicode.com/posts")
       
       
            .then(response => response.json()) // Yanıtı JSON formatında al
            .then(data => {
                // Ekranda gösterilecek alanı al
                const postsDiv = document.getElementById("posts");
                postsDiv.innerHTML = ""; // Her tıklamada eski veriyi temizle

                // Rastgele bir veri seçmek için Math.random() kullan
                const randomIndex = Math.floor(Math.random() * data.length);
                const randomPost = data[randomIndex];

                // Rastgele veriyi ekranda göster
                const postDiv = document.createElement("div");
                postDiv.innerHTML = `
                    <h5>${randomPost.title}</h5>
                    <p>${randomPost.body}</p>
                `;
                postsDiv.appendChild(postDiv);
            })
            .catch(error => {
                console.error("Veri çekme hatası:", error);
                alert("Veri çekme sırasında bir hata oluştu.");
            });
    });
});





// anlamadım

    function array1() {
        // Önceki event listener'ı kaldır
        const form = document.getElementById('basvuruFormu');
        // Yeni event listener ekle
        form.removeEventListener('submit', formKontrol);
       
        form.addEventListener('submit', formKontrol);
    }
    
    // DOMContentLoaded event'ini bir kere dinle
    document.addEventListener("DOMContentLoaded", function() {
        array1();
         // Diğer DOMContentLoaded işlemleri...
    }, { once: true }); // once: true ile sadece bir kez çalışmasını sağla
    





/* İlçe-Mahalle Kısmı */


document.addEventListener("DOMContentLoaded", function () {
    const ilceDropdown = document.getElementById("ilce");
    const mahalleDropdown = document.getElementById("mahalle");

   

    let ilceler = [];
    let mahalleler = [];

    // JSON dosyalarını yükledi
    Promise.all([
        fetch("ilceler.json").then(res => res.json()),
        fetch("mahalleler.json").then(res => res.json())
    ]).then(([ilceData, mahalleData]) => {
        ilceler = ilceData;
        mahalleler = mahalleData;


        /*alfabetik sıra */
        ilceler.sort((a,b) => a.Name.localeCompare(b.Name));

        // İlçeleri dropdown'a ekledi
        ilceler.forEach(ilce => {
            const option = document.createElement("option");
            option.value = ilce.Name; // JSON'daki ilçe ismi
            option.textContent = ilce.Name;
            ilceDropdown.appendChild(option);
        });
    }).catch(error => console.error("JSON Yükleme Hatası:", error));

    // İlçe değiştiğinde mahalleleri yüklesin
    ilceDropdown.addEventListener("change", function () {
        const secilenIlce = ilceDropdown.value;

        // Önce mahalle dropdown'unu temizlesin
        mahalleDropdown.innerHTML = '<option value="">Mahalle Seçiniz</option>';

        if (secilenIlce) {
            // Seçilen ilçeye ait mahalleleri bul
            const ilgiliMahalleler = mahalleler.filter(m => m.District_Name === secilenIlce)
            .sort((a, b) => a.Name.localeCompare(b.Name));;

            // Mahalleleri dropdown'a ekle
            ilgiliMahalleler.forEach(mahalle => {
                const option = document.createElement("option");
                option.value = mahalle.Name;
                option.textContent = mahalle.Name;
                mahalleDropdown.appendChild(option);
            });
        }
    });
});
