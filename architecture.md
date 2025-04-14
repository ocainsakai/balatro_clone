# Game Architecture Overview

## Layer Breakdown

- **GameController**: Quản lý game loop, chuyển cảnh, input
- **GridSystem**: Quản lý lưới 5 lá bài + hệ thống tính toán hand
- **CardSystem**: Quản lý thông tin lá bài, tạo bài, nâng cấp
- **ComboSystem**: Quản lý logic tính điểm nâng cao
- **EffectSystem**: Quản lý power-up, trạng thái đặc biệt
- **UISystem**: Hiển thị điểm, UI chọn bài, thông báo
- **SaveSystem**: (Tuỳ chọn) Lưu tiến độ, config, seed

## Luồng dữ liệu chính

