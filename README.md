# cqrs-with-mediatr-package
* Single Responsibility perpestifine dayanıyor. Her bir command her bir query tek bir iş yapmakla yükümlü.
* Eğer uygulamanda çok fazla değişiklik söz konusu oluyorsa yine kullanmanda fayda var.
* Her bir query her bir command birbirinden tamamen isole birbirini etkileri yok dolayısıyla bir query veya commandinda yapilacak degisiklik sadece orayi etkileyecek.
* Command ve Queryleri databaselerini ayirmakla kalmayip uygulama olarakda ayirabilir ve read heavy bir uygulamaysan query applicationi diledigin gibi scale edebilirsin. Ayni durum command senaryosunda da geçerlidir.
* MediatR kütüphanesi ile crosscut(logging,authentication,exception handling, performance monitoring) yonetimi yapmamıza imkan sunan pipeline davranışları olan behaviours yeteneğide sağlamaktadır.Burda bir trick var. Container'a behavioursları register ettiğin sıra oldukça önemli. Register edilen sıra ile behaviourlara request yönlenecektir.
