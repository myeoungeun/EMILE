using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoSingleton<CoroutineRunner>
{
    //인스턴스만 쓰려고 만든거...
    //총알의 상태(현재 탄창, 남은 총알, 풀 등)는 Bullet / AttackBase가 관리 → 씬 전환 시 초기화 가능
    //DOT 코루틴 실행은 별도의 항상 활성화된 싱글톤(CoroutineRunner)에서만 실행 → 총알 오브젝트가 풀링되거나 비활성화돼도 코루틴이 중단되는 문제 방지
}