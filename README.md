# Break or Burnout
2024년 1학기 공개SW프로젝트 7조 칠전팔기

# Commit 규칙
|제목|설명|
|---|---|
|Feat:|새로운 기능 추가|
|Fix:|버그 수정|
|Style:|코드 형식 변경 및 세미콜론 추가|
|Comment:|주석 생성, 수정 및 삭제|
|Docs:|문서 수정|
|Rename:|파일 및 폴더명 변경|

주 매니저 클래스 아래에 DataManager라는 클래스를 두고 이 클래스를 이용하여 Json파일의 값을 가져올 것임. 
만약 Json 파일의 값이 필요하면 다음처럼 Managers.Data.StatDict라고 쳐서 가져오면 됨, Stat는 파일 포맷 형식(Json 데이터를 가져올 형태)이고 int는 키값
Dictionary<int, Stat> dict = Managers.Data.StatDict;
Stat 형식을 보고 가져오고 싶은 것을 가져오면 됨, Stat의 hp 변수를 가져오고 싶다면
dict[키값].hp 이런 식으로 가져옴
파일 포맷을 보고 dict[키값]을 하면 파일 포맷의 특정 클래스를 가져오는 것
