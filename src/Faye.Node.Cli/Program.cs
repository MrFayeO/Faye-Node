Network n = new();
PeerManager pm = new();
pm.Bind(n);
pm.Run();
await n.Run();