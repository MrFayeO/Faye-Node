public interface INetworkEvent
{
    public event Action<Node> OnNodeConnected;
    public void AddNewPeer(string addr);
}