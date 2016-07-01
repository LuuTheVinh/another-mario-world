using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;

public class Floor : MonoBehaviour, ISwitchable {

    // type của mỗi sector trong map.
    // -1 là vị trí player đã phá.
    // value của của mỗi type ứng với index của _source_Object;
    public enum eType
    {
        hard_brick,
        normal_brick,
        coin_brick,
        boomerang_brick,
        fire_brick
    }

    public enum eStatus
    {
        normal, // lúc player chưa đi qua
        closed  // lúc player đã đi qua
    }

    private const int MAX_LENGHT = 20;

    private static List<int[]> FLOOR_MAP = new List<int[]>(){
        new int[] {1,1,1,1,1,     1,1,1,1,1,     1,1,0,1,1,      1,1,1,1,1},    // F0
        new int[] {1,1,2,1,0,     1,1,1,1,1,     2,1,1,1,1,      1,1,1,1,1},    // F1
        new int[] {1,1,1,2,1,     1,0,1,1,1,     1,0,1,2,1,      1,2,1,1,1},    // F2
        new int[] {1,2,1,1,2,     1,1,1,1,0,     1,1,2,1,2,      1,1,1,1,1},    // F3
        new int[] {1,1,1,1,1,     0,1,1,1,1,     3,1,1,2,1,      1,1,2,1,1},    // F4
        new int[] {4,1,1,2,1,     1,1,1,1,1,     0,2,1,2,1,      0,1,1,2,1},    // F5
        new int[] {1,2,1,2,1,     1,2,1,0,1,     1,2,1,1,1,      1,2,1,1,1},    // F6
        new int[] {0,0,2,1,1,     1,1,2,1,2,     1,1,2,1,1,      1,1,1,0,0},    // F7
        new int[] {0,0,0,0,1,     1,1,1,1,1,     1,1,1,1,1,      1,0,0,0,0},    // F8
        new int[] {0,0,0,0,0,     0,1,3,1,5,     1,4,1,1,0,      0,0,0,0,0},    // F9
        new int[] {1,1,1,1,1,     1,1,1,1,0,     1,1,1,1,1,      1,1,1,1,1},    // F10
            
    };

    public GameObject[] _source_Object;
    public GameObject _floor_switcher;
    public int _floor;

    private int[] map = null;
    private eStatus _status;
    private int _losed_elem;

    private List<GameObject> _elems = new List<GameObject>(MAX_LENGHT);
	// Use this for initialization
	void Start () {
        map = initmap_data(_floor);
        initmap(map);
        _status = eStatus.normal;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}


    private static int[] initmap_data(int flr)
    {
        var temp = new int[MAX_LENGHT];
        for (int i = 0; i < MAX_LENGHT; i++)
        {
            temp[i] = Floor.FLOOR_MAP[flr][i];
        }
        return temp;
    }

    private void initmap(int[] data)
    {
        for (int i = 0; i < MAX_LENGHT; ++i)
        {
            if (data[i] == -1)
                continue;
            var map_element = (GameObject)Instantiate(
                _source_Object[data[i]],
                this.transform.position + Vector3.right * i,
                this.transform.rotation);
            map_element.transform.parent = this.transform;
            if (data[i] != 0)
            {
                map_element.AddComponent(typeof(FloorElement));
                map_element.GetComponent<FloorElement>()._parent = this;
                map_element.GetComponent<FloorElement>()._index = i;
            }
            _elems.Add(map_element);
        }
    }

    private void updatAfterHit()
    {
        for (int i = 0; i < MAX_LENGHT; ++i)
        {
            if (map[i] == -1)
                continue;
            var map_element = (GameObject)Instantiate(
                _source_Object[map[i]],
                this.transform.position + Vector3.right * i,
                this.transform.rotation);
            map_element.transform.parent = this.transform;

        }
    }

    public void block_all(int lose)
    {
        _losed_elem = lose;
        //this.map = new int[MAX_LENGHT];
        //map[lose] = -1;
        for (int i = 0; i < MAX_LENGHT; i++)
        {
            //map[i] = (map[i] == 1) ? 0 : map[i];
            if (map[i] > 2) // 0  là harbrick, 1 là normalbrick, 2 là coinbrick 
                if (_elems[i].GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Discovered"))
                    continue;
            map[i] = 0;
        }
        map[lose] = -1;
        for (int i = 0; i < MAX_LENGHT; i++)
        {
            if (map[i] == 0)
            {
                Destroy(_elems[i]);
                var elem = (GameObject) Instantiate(_source_Object[0],
                    this.transform.position + Vector3.right * i,
                    this.transform.rotation);
                elem.transform.parent = this.transform;
                _elems[i] = elem;
            }
        }
        //initmap(map);
        _status = eStatus.closed;

        Vector3 temp = Vector3.right * (lose - 1.5f);
        Vector3 v = temp + this.transform.position;
        initSwitcher(v);

        temp = Vector3.right * (lose + 1.5f);
        v = temp + this.transform.position;
        initSwitcher(v);
        /////


    }

    private void clear_all_child()
    {
        foreach (Transform item in this.transform.GetComponentInChildren<Transform>())
        {
            Destroy(item.gameObject);
        }
    }

    void OnDestroy()
    {
        _status = eStatus.closed;
        clear_all_child();
    }

    private void initSwitcher(Vector3 pos)
    {
        var obj = (GameObject)Instantiate(
            _floor_switcher,
            pos,
            this.transform.rotation);
        obj.transform.parent = this.transform;
        obj.GetComponent<Switch>()._listObject = new GameObject[1] { this.gameObject };
    }

    public void _switch_on()
    {
        var elem = (GameObject)Instantiate(_source_Object[0],
            Vector3.right * _losed_elem + this.transform.position,
            this.transform.rotation);
        elem.transform.parent = this.transform;
    }

    public class FloorElement : MonoBehaviour
    {
        public Floor _parent;
        public int _index;
        void OnDestroy()
        {
            if (_parent._status == eStatus.normal)
                _parent.block_all(_index);
        }
    }

}
