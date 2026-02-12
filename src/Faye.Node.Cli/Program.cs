Network n = new();
PeerManager pm = new();
pm.Bind(n);
n.Bind(pm);
var t1 = n.Run();
var t2 = pm.Run();

await Task.WhenAny(t1, t2);