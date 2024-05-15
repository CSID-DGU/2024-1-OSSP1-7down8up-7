public enum MonsterState
{
    Idle, // 대기 상태

    Chasing, // 플레이어 추적 상태. OverlapSphere 으로 Trigger 발동되면 Chasing 으로 변화

    Roaming, // 랜덤으로 운동. 이동 상태는 랜덤으로 정해지되, 자연스러운 움직임을 위해, 방향이 꺽이지 않고

    Attacking, // 공격 상태
    Dead, //죽음 상태 
}