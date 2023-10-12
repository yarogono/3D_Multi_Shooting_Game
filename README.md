# 2D 프로젝트
---

# 구현한 기능
- 서버와 DB(MySQl) 연동
  - MySQL을 로컬 환경에 설치
- EntityFrameWork Core를 사용해 마이그레이션
  - Connection String을 config.json 파일로 옮겨 Git에 올라가지 않도록 처리
- 회원 가입 기능
  - 회원가입 UI에서 ID, Password 입력 후 회원 가입 버튼 클릭 시 서버로 패킷 전송
  - 전송된 패킷에서 비밀번호는 Sha256 알고리즘으로 암호화
  - ID와 암호화된 비밀번호를 MySQL 데이터베이스에 저장
