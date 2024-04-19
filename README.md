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

주 매니저 클래스 아래에 DataManager라는 클래스를 두고 이 클래스를 이용하여 Json파일의 값을 가져올 것이다.

[데이터 읽어오는 방법]

1.우선 파일 포맷을 살펴본다.  //Data_Contents라는 이름의 스크립트 파일(이름은 바뀔 수 있음)

파일 포맷을 먼저 보는 이유는 Json으로 가져오는 값은 리스트 형식이고 그 리스트에 어떤 값들이 어떤 이름으로 들어가 있는 지 확인하기 위함이다.

2.다음처럼 매니저 클래스를 이용하여 데이터 객체를 가져온다.

Dictionary<int, Stat> dict = Managers.Data.StatDict;

3.다음과 같이 데이터 객체에 키값을 넣어서 원하는 몬스터 정보를 가져온다.

dict[키값].hp

//키값을 통해 가져온 리스트 중 원하는 변수(hp)에 접근한 것이다.



