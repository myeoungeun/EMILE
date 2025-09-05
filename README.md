![banner](https://github.com/user-attachments/assets/154ac25f-8745-4bd4-bc87-f4a19f1dc26e)

# 목차
---
### 🎮 [프로젝트 소개](#프로젝트-소개)
---
### 🖥️ [개발 기간](#개발-기간)
---
### 🖥️ [깃허브 구성](#깃허브-구성)
---
### 🖥️ [사용된 기술 소개](#사용된-기술-소개)
---
### 😟 [트러블 슈팅과 해결 과정 소개](#트러블-슈팅과-해결-과정-소개)
---
### 🤝 [팀원 소개](#팀원-소개)
---
<br><br><br>

# 프로젝트 소개

## **✍️** [Figma 링크](https://www.figma.com/board/dKdvdpFbEZ4sBSMkXWzEzS/%EC%A0%9C%EB%AA%A9-%EC%97%86%EC%9D%8C?node-id=0-1&p=f&t=fnVup5KXCERHfWW3-0)

## **👨‍👩‍👧‍👦** [Team Notion 링크](https://www.notion.so/25a2dc3ef514819ab176e7b6345b8b87?pvs=21)

## **✍️** [기획서 링크](https://www.notion.so/3-25b3482df94c809b94cec463b7f262e3)

## **✍️** [담당 업무 로드맵 링크](https://www.notion.so/EMILE-2613482df94c806e92dcfb0f92accb4a)

## **✍️** [QA 테스트](https://www.notion.so/ELIME-QA-264d0881b9c48054b336f18561de782e)


![image (19).png](https://github.com/user-attachments/assets/5f915775-73ea-438e-b1b3-af417c23ef77)

| **게임명** | E.M.I.L.E. |
| --- | --- |
| 장르 | 디스토피아적 사이버펑크 세계관을 배경으로 한 2D 플랫폼 액션 게임 |
| 동작 환경 및 운영체제 | PC, Window |
| 개발 게임 엔진 | Unity 2022.3.17f1 |
| 사용언어 및 IDE | C#, Visual Studio 2022 |
| 버전 관리 | Github |
| 개발 기간 | 2025.09.01 ~ 2025.09.05 |
<br>

📜 **[목차로 돌아가기](#목차)**
<br><br><br>

# 개발 기간
<h2>📆 프로젝트 개발 일정</h2>

<h3>🗓️ 사전 기획 및 개발 <small>(09/01 월)</small></h3>
<ul>
  <li>프로젝트 주제 선정 및 범위 설정</li>
  <li>레퍼런스 게임 분석 및 핵심 메커니즘 파악</li>
  <li>개발 일정 및 역할 분담</li>
  <li>기본 시스템 구현(씬/코드/폴더 분리)</li>
</ul>

<br>

<h3>🚀 기능 개발 <small>(09/02 화 ~ 09/03 수)</small></h3>
<ul>
  <li>캐릭터 애니메이션 제작 및 움직임/점프 개발</li>
  <li>맵 디자인 초안 작성 및 구현</li>
  <li>장애물 배치 및 패턴 제작</li>
  <li>UI 프레임워크 구축</li>
  <li>코인 수집 및 점수 시스템 구현</li>
  <li>캐릭터 커스터마이징 기능 개발</li>
</ul>

<br>

<h3>🧪 기능 통합 <small>(9/04 목)</small></h3>
<ul>
  <li>리팩토링 및 각각의 코드 통합</li>
  <li>오류 수정 및 피드백</li>
</ul>

<br>

<h3>🏁 최종 완성 및 마무리 <small>(09/05 금)</small></h3>
<ul>
  <li>오류 수정 및 마무리</li>
  <li>발표 PPT 제작</li>
  <li>최종 발표 준비 및 시연</li>
</ul>

<br>

📜 **[목차로 돌아가기](#목차)**
<br><br><br>

# 깃허브 구성

<h2>📁 GitHub Repository 구조</h2>
<ul>
  <li><strong>master (root)</strong>
    <ul>
      <li><strong>develop</strong>
        <ul>
          <li>feat/player</li>
          <li>feat/attack</li>
          <li>feat/map</li>
          <li>feat/monster</li>
          <li>feat/AsynLoadScene</li>
        </ul>
      </li>
      <li><strong>origin</strong>
        <ul>
          <li>origin/Manager</li>
        </ul>
      </li>
    </ul>
  </li>
</ul>
<br>
  
📜 **[목차로 돌아가기](#목차)**
<br><br><br>

# 사용된 기술 소개
<br>
  
📜 **[목차로 돌아가기](#목차)**
<br><br><br>

# 트러블 슈팅과 해결 과정 소개

1. 리팩토링 + 구조 통합 과정 문제
- 원인 : Addressable 기반 코드 활용 미숙 + 탄 수는 `Dictionary<BulletData, int>`로 관리했는데,  `currentBullet`을 바꾸는 시점과 Dictionary에서 조회하는 시점이 달라서  탄 수가 안 바뀌거나 고정된 값만 나옴
- 문제 흐름
	1. 리팩토링 : 기존 Attack 클래스가 플레이어 전용 + 총알 관리까지 맡고 있어 재사용성/독립성이 떨어짐. → `AttackBase` / `PlayerAttack` 으로 분리.
	2. 연동 문제 : Player 코드에서 사용할 수 있도록 `MonoBehaviour`를 제거 → Inspector 연결 불가, Null 에러 다수 발생.
	3. Addressable 난항 : `BulletData` ScriptableObject 불러오는 순서 꼬임 + 탄 수(Dictionary)와 현재 총알(currentBullet) 불일치 문제 → 총알 종류를 변경해도 총알 개수가 고정됨 or 특정 총알만 발사되는 등의 문제 발생
- 해결 방법
	- Player가 객체를 직접 new로 생성해서 데이터를 코드에서 전달
	- Dictionary로 남은 탄 수 저장/복원
	- Addressable 비동기 로딩이 끝난 후에 currentBullet 갱신 및 초기 세팅 함수 호출
- 결과 : 성공적인 리팩토링 및 통합이 이루어졌고, 총알 관련 문제도 모두 해결함.

<br>
2. 플레이어의 총알과 보스 사이의 충돌 발생x
- 원인 : 보스의 경우 콜라이더의 회전이 필요하여 하위에 콜라이더 전용 오브젝트를 구분하였기에 발생한 것으로 추정
- 사실 수집 : 충돌은 실행되지만 IDamageable 객체가 Null로 확인됨, 터렛과는 정상적으로 상호작용이 작동함
- 결과 : 기존 캡슐콜라이더를 박스 콜라이더로 변경하고 최상위 오브젝트에 컴포지트 콜라이더를 추가하여 정상적으로 작동하는 것을 확인함


<br>
3. 플레이어의 다중 중복 상태
- 원인 : 움직임,벽잡기,착지의 중복된 값이 동시에 일어나게 되어 StateMachine값에 의도치 않은 값이 들어가게 됨
- 조치 : 추론한 원인을 토대로 벽잡기를 바닥에 닿지 않은 시점으로 한정함
- 결과 : 벽잡기를 바닥에 닿지 않은 시점으로 한정하여 구현,정상 작동을 확인함
<br>


<br>

📜 **[목차로 돌아가기](#목차)**
<br><br><br>

# 팀원 소개

| 직책 | 이름 | 담당 업무 | 개인 블로그 | 개인 깃허브 |
| --- | --- | --- | --- | --- |
| 개발 팀장 | 이명은 | 공격 구현 |  |  |
| 개발 팀원 | 허윤 | 플레이어 담당 |  |  |
| 개발 팀원 | 김남진 | 몬스터 담당 |  |  |
| 개발 팀원 | 오승엽 | 맵/UI 담당 |  |  |
| 기획 팀원 | 박용규 | 기획 담당 |  |  |
| 기획 팀원 | 박진우 | 기획 담당 |  |  |
<br>

📜 **[목차로 돌아가기](#목차)**
