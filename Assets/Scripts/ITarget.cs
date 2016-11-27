using System;
using System.Collections;
using System.Linq;
using System.Text;

interface ITarget {
    int TotalHealth { get; set; }
    IEnumerator MovePosition();
}

