<div align="center">
    <h1>Pemanfaatan Pattern Matching dalam Membangun Sistem Deteksi Individu Berbasis
 Biometrik Melalui Citra Sidik Jari</h1>
    <h3>Tugas Besar 3 IF2211 Strategi Algoritma</h3>
    <p>oleh kelompok ResmiTamatStima</p>
    
<img src="./src/Images/logokelompok.jpg" alt="Example screenshot" width="400"/>
    <br/>
    <br/>
</div>
Pattern matching merupakan teknik penting dalam sistem identifikasi sidik jari. Teknik ini digunakan untuk mencocokkan pola sidik jari yang ditangkap dengan pola sidik jari yang terdaftar di database. Algoritma pattern matching yang umum digunakan adalah Bozorth dan Boyer-Moore. Algoritma ini memungkinkan sistem untuk mengenali sidik jari dengan cepat dan akurat, bahkan jika sidik jari yang ditangkap tidak sempurna.
Dengan menggabungkan teknologi identifikasi sidik jari dan pattern matching, dimungkinkan untuk membangun sistem identifikasi biometrik yang aman, handal, dan mudah digunakan. Sistem ini dapat diaplikasikan di berbagai bidang, seperti kontrol akses, absensi karyawan, dan verifikasi identitas dalam transaksi keuangan. 

## Penjelasan Algoritma 👁️
### 1. Knuth-Morris-Pratt
Algoritma KMP menggunakan struktur data tambahan berupa LPS array (Longest Proper Prefix which is also suffix). LPS array berisi panjang maksimum dari prefix yang juga merupakan suffix. Nilai LPS[i] adalah panjang prefix terpanjang yang juga merupakan suffix dari pattern hingga indeks i. Dengan menggunakan LPS array, algoritma KMP dapat memajukan pencarian di text tanpa mengulangi pengecekan pada karakter yang telah dicek. Langkah-langkah untuk mengimplementasikan algoritma KMP adalah sebagai berikut.
1. Buat LPS array dari pattern. LPS array berisi panjang maksimum dari prefix yang juga merupakan suffix. Nilai LPS[i] adalah panjang prefix terpanjang yang juga merupakan suffix dari pattern hingga indeks i.
2. Mulai mencocokkan pattern dan text. Jika karakter pattern dan text sama, maju ke karakter berikutnya pada pattern dan text.
3. Jika karakter pattern dan text tidak sama, lakukan hal berikut:
   Jika indeks pattern tidak nol, kembali ke karakter sebelumnya pada pattern menggunakan LPS array. Ini tidak mengubah posisi karakter pada text.
   Jika indeks pattern nol, maju ke karakter berikutnya pada text.

### 2. Boyer-Moore
Langkah-langkah dalam menerapkan algoritma Boyer-Moore adalah sebagai berikut.
1. Buat tabel Bad Character Heuristic. Tabel ini berisi jarak dari karakter terakhir pattern ke lokasi karakter tersebut terakhir kali muncul dalam pattern.
2. Buat tabel Good Suffix Heuristic. Tabel ini berisi jarak lompatan jika ada suffix yang cocok dalam pattern.
3. Mulai mencocokkan pattern dan text dari karakter terakhir pattern. Jika karakter pattern dan text sama, maju mundur ke karakter sebelumnya pada pattern dan text.
4. Jika karakter pattern dan text tidak sama, atau telah mencapai awal pattern dan semua karakter cocok, lakukan hal berikut:
Hitung jarak lompatan berdasarkan tabel Bad Character Heuristic dan Good Suffix Heuristic.
Pilih jarak lompatan terjauh dan maju sejauh itu dalam text.

Algoritma Boyer-Moore sangat efisien, terutama pada teks yang panjang dan pattern yang pendek, karena sering kali dapat melompat lebih jauh dibandingkan algoritma pencarian string lainnya

### 3. Regular Expression
Regex yang diimplementasikan dalam program ini melakukan handle untuk angka dan huruf kapital, berikut ketentuannya mengurut sesuai prioritas:
1. Jika huruf pada suatu kata kapital semua, jangan ubah menjadi nonkapital
2. Apabila pada satu kata hanya ada huruf, jangan ubah menjadi kata
3. Apabila ada kapital pada awal kata, jangan ubah menjadi nonkapital
4. Ubah semua kapital menjadi nonkapital
5. ubah semua angka menjadi huruf

## Kebutuhan Program 🔨
Visual Studio IDE 2022

## Cara Menjalankan 🏃🏾‍♂️

1. Clone repository ini pada Visual Studio

2. Buka file .sln

3. Click tombol run/play atau tekan F5

## Creator 🔥
| NIM | Nama | Linkedin |
| :---: | :---: | :---: |
| 13522135 | Christian Justin Hendrawan | [LinkedIn](https://www.linkedin.com/in/christian-justin-388aab253/) |
| 13522142 | Farhan Raditya Aji | [LinkedIn](https://www.linkedin.com/in/farhan-raditya-b807272a0/) |
| 13522161 | Mohamad Akmal Ramadan | [LinkedIn](https://www.linkedin.com/in/akmalrmn/) |
