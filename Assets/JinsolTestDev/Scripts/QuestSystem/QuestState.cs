namespace Jinsol
{
    public enum QuestState
    {
        REQUIREMENTS_NOT_MET, // 퀘스트 수락 불가
        CAN_START, // 퀘스트 수락 가능
        IN_PROGRESS, // 퀘스트 진행중
        CAN_COMPLETE, // 퀘스트 완료 가능
        COMPLETED // 퀘스트 완료
    }

}