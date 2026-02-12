public class PeerManager
{
    List<string> nodes = new List<string>
{
"25miqadfzmfkt6s5lg6alb7jhztqkxpq66hefqhj7vd4mmvuhnvullad.onion",
"2cbogleytj3doq3crhuhbpe6q4lgwcyex7el4yjku4kduhfnywuaasad.onion",
"2t55iwnvo2cdtrceapjxtrturdnwbjdfydcilhym3okvl3ibunywgdad.onion",
"3clb2rhr7h4ma2a5m5g6abgfle3mi6zimjl7ig3x357xbmexci2yvwqd.onion",
"3ievyup5wzj6i6vj22sknwfv4gviuu2wn5bdlfizhq4hfkwvcp2bjqyd.onion",
"4dbr3hiuull7wwqsu3wepieh3ydl5j245hcnx7tk4squql7ehaid7jad.onion",
"4dutavdbsbt4h3h5s4r2bivtbu77ne4zrimcnb72idfvtxeajlzwfjyd.onion",
"4gt4nseugqtfndducg57ymxb2rlp3tki2v72b54x42t7ezkh4ofh2uid.onion",
"4hzfyjs6n37c35qsw33l4f4rr6j4ltcryzeaf7asevhvfarp4qp5l2yd.onion",
"4mjtyuojn6qacxm4svrkw236pkdqvlmix6mkgtpksd2bqc3a64kapsqd.onion",
"4pbqjruwzbhbxl3dazvcntmoi5rg5g64xnzdoizzkum4obzedmb42tqd.onion",
"5w6iun65dmndu33xxbyaoz7arufllx5dbgjh7ob74iyc5ab4xlqbajqd.onion",
"5y6aavr2dulfvehtaa6ldyub5o6qrsj7pg3uas25zb5l32u6boggo7ad.onion",
"6ka7kz6liztyvd6tt3vmxjqu45tvb7rfxcraikhjlqj6v37hqovgarad.onion",
"6rzxtwxun24scct5qhicyck2sjs44f3mwudpfzatzgwtk54c5n6iixyd.onion",
"73pgqtrxf3oq6fuhmonjlgahy5hvrum5t2evla6pinl4do4oxznkrtid.onion",
"7r5f4o3jxjppbwizuyo4hxvc4ym56dqmtpdawycakbpxpj5vaigsylad.onion",
"a4itz2jkdz6ryfgt43h7wisejjk2mog5lyiuigsoaj2ylkclp7zxzyqd.onion",
"a6lc66mohf4n4r5bsfw3qbeydgv4uqavql22ptawusobmxts7q45ueyd.onion",
"a7dwyqqwtredwk7wfs2is2mp6r4n3nij2bdtk5ite34fle7yzzvj3ayd.onion",
"acnnpqt3mpvvrfz57pd7qjrgmqobnoyoduw2jduzl3u7mnqttg4etgid.onion",
"adapnqwapfv6b6hz3lacr2zm6mpkzqr2jfjeer52qtexkcisz4t3rlqd.onion",
"af5gd6buyjzsvhoqwa3lcuy3f2wzqzxeogd2xkpvaeoznkjje5lkjpyd.onion",
"ayj2etwi6bkcqmya2efwhl4adqsmkevcfnhfmdresz3ifrlpvjcux3id.onion",
"b2zvmotrkxga5hvfijlcgpzy2weszi644gny2wj4jadqns6kwpsrgbad.onion",
"b4oq7iq2ysb6yb6qfduvc45rzfpj3ged3gtx4g3bq35o2ocv55jp3kqd.onion",
"b6h5hi5esddz3xa432gb46o2wgzqw7n5wram7n2swfdjtcdjz33a7vad.onion",
"bejtjqh4zjxzgfszj6zbybcdugs5knp47jtrt533grqenbphuiifqvid.onion",
"c4b4al7t3mh4bunmgy3qr7on3l23g6o62mmk4ppmfbbisqhhpc4qj2yd.onion",
"cdawjqozako4yphcyeja5gm6h6otbseeb47wfaww75hly43qooeaggyd.onion",
"cvhs25qgqc3la5zvdz4iighyj7vvt2dltb6acru3o6fakpmmj2blvgyd.onion",
"cw2vgttrwxmxcukvt6iboldvraxlbqbboxm44ynb524vjnsow2ncx7id.onion",
"cxndb42tqfnhnm56sbybxjrvag6ubpxcuerumdxhgmd2znjebjyh7zyd.onion",
"d46jfui2qslczwigljza6ovst3mfs7z7uvkwlvtds5fwdr34nxniw5ad.onion",
"dgehr55cbnvn7d2pn2iafgnz6heijh7d7lezlmuubfll36khaprikiad.onion",
"dsakprajvvs6uczmlsieoy4zwu32jg5ggwrkv7ti67e6rajkg3ttotqd.onion",
"dserwun2ysecqamyjif6qihgv4pfmothsgli247atgqy5zypwkrp64ad.onion",
"dujfuq2g7swnhcgw4ltn74xkmyturl4tbwisjpt77rdmjqawkfdxi7qd.onion",
"e4stjy2bdiebut7zhwbwlyiemout3ynh6ckh7zqu2ij4c6bkqr54quyd.onion",
"eg2kg4yvuyuf7474l72fw5boq5tecglw23bkci2eeo3ha7mjbv7f7wyd.onion",
"eg4uqea6grabztsjvey667ryoixjpljil2tgemjjme2tukwt43x4yfyd.onion",
"elr7tfe2qioc4imumjdwsxw3xietj4d5yczxzfpmw4eqyzxgonymcrad.onion",
"ewsfovxeonkwttka7ko4j4ldwujxzar7gmswtrgbnzy3cpjsfvoidzyd.onion",
"fcrkstepur2hwxf5ciyr76hjudtylj743jw56s3z2mifd5u42xusscad.onion",
"ffkziooj6bu4qeg47i3ffhhhm2fcz7k55ef6ycjxey5jjv3dsz6rtiad.onion",
"fke35mboddfsnwedyovlxvwendewlqis22qxlfaush3c64qbxczhxrad.onion",
"frhjhwtlaqngyuazucaz34xyexk6wcra2oohfazmhjnicqvpjmfgzxqd.onion",
"fzo67rsaittjxjewjznsbnhh3qioy7ghsvmxngoffqmozmiho6y34qid.onion",
"g7giz2cxgzu2wldpa4skjzprji5vckkkbdodnsacdzgkt2z2ysnhqwyd.onion",
"gofrxrt32e5rdb6lhiyu5lyxbhfre5dqzr32mbxufxwub6sxdd4dueid.onion",
"gtcrsi7u734nnqfsd46efmaqyegvuzixaj65xl6h6xosr5mbrw6kanqd.onion",
"h3f6skviyrdwm442x3axwh43vgw74ujqwsv5vokr7l5z6bfciy66ggqd.onion",
"h3hd6i7tkiqgtfa23ohhohkqedhnogry7jd6uyqgnbgcflpfwmgplcyd.onion",
"hacginhadck2omylb3xtlxwe5fkpiv7ygxliqaq75isgtlvnkwihuxid.onion",
"hg7rnlv6h5qmz7klyqztgngn44hrdx73zwufisvuwdbdxopj7aljduqd.onion",
"hu2tft7wxobmfxxmpajplpzwck6slvsiqtrsa7d4nc24rgzykmhijdid.onion",
"i2bzdwbe2e7ybyu2u54ziwi3gsnwg77yiktqn47jpe3clvhdgfgw3vad.onion",
"ib6rpry2qwlhtnrfzquw37uy7yyjemp36i2hqfpa57mvzfhxfqiy4qqd.onion",
"ibrmry7d6dsdvxugeh3yswc4vphnw33i4jnqrns2ncixmtzpmwigwlyd.onion",
"irn4auxi7img5hbwzaxldoabbctk6ym2olehwjspbsbv76u3usrxzxad.onion",
"ivgknomncqt2twn6uv624lq5snav6d2honnuhwo2nv4y2hs67aiyikad.onion",
"j57r5ftwrzxpthgwyp3jmteivdpqqa2tj7rgepweh6wt7l2lt2bthqyd.onion",
"jg2lzvbtsdyfskpfvjp5atzpl423qh6vbl27knkzlhyb6jdxkk6ur7ad.onion",
"jgh67min5lw3djmpozbsuhv64vbf2tteyxu2eziitlvb54wzgbcjsgyd.onion",
"js36luafswdtly2llgr6mgwx33p7d2bhir5ci2cgzxfqnhrkerjil3yd.onion",
"jx24o36zmfyiow7uxbqg6r6pf6ce4ysegz6p42tcdsttcszpm2cmodid.onion",
"jy2qhz26ytj4fcbuywipoqpzclzgvapdn3rameakkoxhy5mjkrblmbad.onion",
"k6dotpdp4yyehq43zmqekzrkrmyss75cpva5kvahkwh6iavmlt4jkyyd.onion",
"kptom6mculx6upmvby7rvz3gxcihcu2jsqjuwxykmzeesnmtnmyashyd.onion",
"kuoy5upglmk6i3or2sjxx5oar6cn7fr4emnojzpbyxwbjgwh5schhcyd.onion",
"l2kny3slv3zhlqxo466cwrottcfd4ojfn7o3mhg5kl4ui4v5cwjkgiyd.onion",
"l4lzy473ui44hyzopj43azgiwf2xwpb2effwo67c6cfkeulhidfnytqd.onion",
"l6qnmbjzxufx4vyrw7nds7w45wbmtmdggngzidghsk736iu5h5agtgyd.onion",
"ldtmfo2e5th7gdnsqdgnw6y2biywdbqtcnxdmbglp24tx3t3uldwu4id.onion",
"lnan6rnf3322vxp7uq3aypkeodogszruddebmw3wktd55d6jwn7cooyd.onion",
"ltnowkzslhvsckx24vxsomhcbkmutolsatp5uxsur4rop3sx4wsctuad.onion",
"maaq22hzbzbu3wnmqad6ed6ykku7ozesdwxnovavrbf3ymkzj634w2yd.onion",
"msk37anaduni3v2e5gqmj4lc4e3qzjqurr7fpnqdfu76xkd4t6atznyd.onion",
"n2czxgldbrvgrr3ihndyvj2kfqoilvn6eddxn37aubtcuu32tgsatlqd.onion",
"osaos22dtshvj2llxkmaatbvwwyw3etktnfh6w57lf57dskcyljr4qyd.onion",
"p7xvq2xyagmt5i7etjfydfeiwoopaqfudcovrwdxqx4kpfrtks4u4sad.onion",
"qj2x6vvxiz7qx36gduteyww6rdyi54vsj7ceonlwiegwk4k7r5gksiqd.onion",
"rd3oq4kkgilo473hwzoiyogbv4h2qxzdqhqza7m2jbhprvdpvjkj5jid.onion",
"rwjlfge4c4kqmlnmvkb3ij5wyuiblhdfr65qyl26w7xbr6eb26xmzdyd.onion",
"sa7wl5vmh6e6w65vs7cmfjusaleb6o3txkwoxibei7x2jwd5qzrc47ad.onion",
"sinp446nuikjyu64idwf3xykrxxfryndyv3c2cpk5xpgj3oruwy3pqid.onion",
"t3pwp5t4dgwwtx5hyz2npeccmajedklglua4j4yye3jcdt4pcjnyekyd.onion",
"t4mez443qzgaerfjzbqk2jq6sonx5r5uzdofjyo36osnqhdzreyvupyd.onion",
"t7x33jufqhdpl3o32y5tby5fxl3drjvargvinp2btb22bixdtygf7nid.onion",
"tgtvxyszt44cggj7ccdjpvrokl3s3qbtlgbihc36y7hn2z7s6usxdbid.onion",
"tpy5ba6dryxw5jxqy5qjcdplyjconf3kb5wvsqmcl4nsizz5d57sy5ad.onion",
"ttozc6un4bmw6q4sj35zswzommbfwzjokfwk3lvemfhwvajy23oothyd.onion",
"tx3dxrw4k54kcoqad5me7cfvz65cc2dqucxgfqtmk6hf5zdko5ts3sid.onion",
"udoxsl6z5auy6zrf7yuwiijzvy7dx3v7h47rwawnoq7aicp6dvnuc3ad.onion",
"ufsdjsi6k7vl7witz2v3vwjazho72deesgxpxoniw72biirsiiekooid.onion",
"ug7aekdvgpcmkb4l2wmtzjpgqmaeoiztkns5uo6ygixru5255hrcbrad.onion",
"uopfw6k6xikey74eqyhfycl4qgmgtj6vp57ylbvwh6yvtky4tynikbid.onion",
"uwo3es2nj7h4bheclp655fhtzspeiiphb6q4wy3yr7fc5gyv5zldhdid.onion",
"v4iubweo5beihtpm7tatqnkiqjsgzgixxkkudlyvjv7cipfxkh5a5yad.onion",
"v5ls3jvm7gdy2qnyv2uf6lyy25qd7xgx74pd7p7hcmmtjugt5r2n7vid.onion",
"vst3lvr2sgw6hbs4nvjiv54pf47xd65mjj7jcx3hghq5br5iutwxayyd.onion",
"vvo7uxam5vwk4677lczg6yeel2b3hv47z3wzyejirahp4a2fpgj6oxid.onion",
"waltgnt7ygtktslls4cqqsvfvpijtk6nbjp6wex3pv4sa5p3tvmpaqad.onion",
"wg7eepsg2tqq5ftcxln6q6viqa5hawebziqvtysg4btaxtss2ivbhqyd.onion",
"wjputexzdk3lao4b5unkjca7cxo2cwtbk7bwgorarrkqjsnbnockyxad.onion",
"x22syft4lmwyatp7s6bu6je5sevv23kaqhh7dfqtzg47m747mpwdjyqd.onion",
"xrehi3kvcrliam7eub7mnqttevnqpdbjib6bh3ienxorfkrpny2yvsyd.onion",
"y5cpxh2bztxzrodbps4rdtjcz3v72krm6vgjbf626vqt3t5yv7d357ad.onion",
"yfg3dgn6csop6rye6ku6gxpnt7chczzplnsg4dredkrvv5uyg22xbpqd.onion",
"yfnoc7aqwxlnjhysazfwe6duop5ccf7ujp665ltojrbscxye4zqaa3qd.onion",
"yif3ln2hekfszcdgvdhranu4u4ikjtpimwwyknsc3positte2hrndeid.onion",
"yk3z544pgayadblevgnytskb44ohjigh355snfbgrv3vmjca2227ecad.onion",
"z4f3dj6djg65hgm7lnrpflcyryjpi7mdpps75wg5ohqdreeaf7wuxeqd.onion",
"zbc2lt5mlqn4p4pdldwjpdvjndb6alnzrxqss7qwqvjkmhhzgvuzcsqd.onion",
"zhp32febcps35lzchq3mggrcamhejexgjj4pi3izwltacjpgj64x25ad.onion",

};

    private Network? _Net;

    public void Bind(Network net)
    {
        _Net = net;
    }

    public async Task Run()
    {
        foreach (var addr in nodes)
        {
            _Net?.AddNewPeer(addr);

        }

        while (true)
        {
            await HandleInboundMsg();
        }

    }

    public async Task HandleInboundMsg()
    {
        var peers = _Net.GetPeers();

        foreach (var entry in peers)
        {
            var node = entry.Value;
            var msg = node.GetInboundMsg();
            if (msg == null)
            {
                await Task.Delay(0);
                continue;
            }

            var header = msg.Header;
            var cmd = Utils.GetStringWithNoPadding(header.Command);

            NetMsg? newMsg = null;
            switch (cmd)
            {
                case "version":
                    var versionPayload = (VersionMsg)msg.Payload;
                    node.setUserAgent(versionPayload.UserAgent);
                    node.SetVersion(versionPayload.Version);
                    break;
                case "verack":
                    var verackHeader = PacketHeader.Create(CommandName.VerAck, []);
                    node.setVerack(true);
                    newMsg = new(verackHeader);
                    break;
                case "ping":
                    var pingPong = (PingPongMsg)msg.Payload;
                    newMsg = new(new PingPongMsg(pingPong.Nonce), CommandName.Pong);
                    break;
                case "pong":
                    break;


            }
            if (newMsg != null)
            {
                node.AddOutboundMsg(newMsg);
            }
            await Task.Delay(0);
        }
    }

    public async Task InitializeNode(Node newNode)
    {
        IBitcoinPayload versionPayload = new VersionMsg(0);
        NetMsg version = new(versionPayload, CommandName.Version);
        if (!newNode.AddOutboundMsg(version))
        {
            Console.WriteLine($"Failed to send Version Message");
        }
        await Task.Delay(0); //Remove warning
    }
}