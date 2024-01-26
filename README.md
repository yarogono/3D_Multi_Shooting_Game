# 혼자 만들어보는 3D 멀티 액션게임
---

3D 탑다운 멀티 액션 게임입니다.
클라이언트부터 게임 서버 엔진, Rest API 서버까지 직접 구현한 프로젝트입니다.

<br/>

# 기술 스택
---

<table>
	<tr>
		<th>분류</th>
		<th>기술</th>
	</tr>
	<tr align="center">
		<td>Language</td>
		<td><img src="https://img.shields.io/badge/CSharp-purple?style=for-the-badge&logo=csharp&logoColor="></td>
	</tr>
	<tr align="center">
		<td>Game Engine</td>
		<td><img src="https://img.shields.io/badge/Unity-black?style=for-the-badge&logo=unity&logoColor=white"></td>
	</tr>
	<tr align="center">
		<td>Server</td>
		<td><img src="https://img.shields.io/badge/ASP.NET Core-blue?style=for-the-badge&logo=.net&logoColor=white"></td>
	</tr>
	<tr align="center">
		<td>DB</td>
		<td><img src="https://img.shields.io/badge/MySQL-4479A1?style=for-the-badge&logo=MySQL&logoColor=white"></td>
	</tr>
	<tr align="center">
		<td>Other</td>
		<td><img src="https://img.shields.io/badge/Entity Framework Core-purple?style=for-the-badge"><br>
		    <img src="https://img.shields.io/badge/Nginx-green?style=for-the-badge&logo=nginx&logoColor=white">
		</td>
	</tr>
</table>

<br/>

# 아키텍처
---

<img src="https://github.com/yarogono/3D_Multi_Shooting_Game/assets/70641418/6668aefb-4f88-44df-bb90-bf37e9607623">

<br/>

# 기술 내용
---

개발을 하면서 만났던 문제 혹은 개선할 점을 작성해봤습니다.  
구현한 내용을 회고하고 좀 더 자세히 공부하고 습득하기 위해 블로그에 정리했습니다.  
(블로그 글 링크를 누르시면 블로그로 이동됩니다.)

<br/>

## 클라이언트
- 캐릭터가 벽을 통과하는 문제를 Raycast를 사용해서 벽을 감지해서 이동하지 못하도록 해결([블로그](https://velog.io/@yarogono/Unity-%EC%BA%90%EB%A6%AD%ED%84%B0%EA%B0%80-%EB%B2%BD%EC%9D%84-%ED%86%B5%EA%B3%BC%ED%95%98%EB%8A%94-%EB%AC%B8%EC%A0%9C))
- 반복적으로 작성되는 싱글톤 코드를 제네릭 클래스를 사용해 통합해서 관리하도록 구현 ([블로그](https://velog.io/@yarogono/Unity-%EB%B0%98%EB%B3%B5%EB%90%98%EB%8A%94-%EC%8B%B1%EA%B8%80%ED%86%A4-%EC%BD%94%EB%93%9C-%ED%86%B5%ED%95%A9-%EC%B2%98%EB%A6%AC%ED%95%98%EA%B8%B0))
- GameObject.Find()가 씬의 모든 GameObject와 문자열로 비교하는 성능이슈와 런타임 오류 위험성을 인지하고 개선([블로그](https://velog.io/@yarogono/Unity-GameObject.Find%EC%9D%98-%EC%84%B1%EB%8A%A5-%EC%9D%B4%EC%8A%88))
- 로컬에서 멀티플레이 게임을 테스트하기 위해 다중 클라이언트를 빌드 후 실행 할 수 있도록 구현([블로그](https://velog.io/@yarogono/C-Unity-%EB%A9%80%ED%8B%B0%ED%94%8C%EB%A0%88%EC%9D%B4-%ED%99%98%EA%B2%BD-%EC%84%B8%ED%8C%85))

<br/>

## 서버
- 집에 있는 여분의 컴퓨터의 우분투 환경에 C# 게임 서버 환경 구축([블로그](https://velog.io/@yarogono/C-%EB%82%A8%EB%8A%94-%EC%9C%88%EB%8F%84%EC%9A%B0-%EC%BB%B4%ED%93%A8%ED%84%B0%EC%97%90-%EA%B2%8C%EC%9E%84-%EC%84%9C%EB%B2%84-%EB%B0%B0%ED%8F%AC))
- 해외에서 해킹 시도를 하는 것을 로깅 시스템을 통해 확인하고, NGINX를 사용해 해외 IP 차단해 대처([블로그](https://velog.io/@yarogono/NGINX%EB%A5%BC-%EC%82%AC%EC%9A%A9%ED%95%B4-%ED%95%B4%EC%99%B8-%EC%84%9C%EB%B2%84-%EC%B0%A8%EB%8B%A8%ED%95%98%EA%B8%B0%EC%84%9C%EB%B2%84-%EB%B3%B4%EC%95%88))
- DataManager를 구현해 기획적으로 수정 소요가 많은 아이템 데이터를 Json으로 저장하고 불러오도록 구현([블로그](https://velog.io/@yarogono/%EA%B2%8C%EC%9E%84-%EC%95%84%EC%9D%B4%ED%85%9C-%EB%8D%B0%EC%9D%B4%ED%84%B0%EB%A5%BC-%EC%96%B4%EB%94%94%EC%84%9C-%EA%B4%80%EB%A6%AC%ED%95%98%EB%A9%B4-%EC%A2%8B%EC%9D%84%EA%B9%8C%EC%84%9C%EB%B2%84-%ED%81%B4%EB%9D%BC%EC%9D%B4%EC%96%B8%ED%8A%B8))
