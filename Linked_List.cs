using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Trinh_duyet.Linked_List;

namespace Trinh_duyet
{
    class Linked_List
    {
        public class Node_Url
        {
            public string url;
            public Node_Url next;
            public Node_Url prev;

            public Node_Url()
            {
                this.url = "";
                this.next = null;
                this.prev = null;
            }
            public Node_Url(string x)
            {
                this.url = x;
                this.next = null;
                this.prev = null;
            }
        }
    }

    class List
    {
        public Node_Url head;
        public Node_Url tail;
        public List()
        {
            this.head = null;
            this.tail = null;
        }
        public void AddLast(string x)
        {
            Node_Url newNode = new Node_Url(x);
            if (this.head == null && this.tail == null) //Nếu danh sách hiện tại rỗng
            {
                //Node l = new Node();
                
                this.head = newNode;
                this.tail = newNode;
            }
            else //Danh sách có phần tử thì thêm vào cuối danh sách
            {
                Node_Url p = this.tail; //Gán p bằng node đầu danh sách là node l
                //while (p.next != null) //Thực hiện next tới để tìm về cuối danh sách
                //{
                //    p = p.next;
                //}
                //Tạo node mới, gán giá trị và trỏ next của node cuối về node mới.
                //Node_Url newnode = new Node_Url();
                //newnode.url = x;
                newNode.next = null;
                p.next = newNode;
                newNode.prev = p;
            }
        }

        public Node_Url FindLast(string x)
        {
            Node_Url p = this.head;
            Node_Url result = new Node_Url();
            while (p != null)
            {
                if(p.url == x)
                {
                    result = p;
                }
                else
                {
                    p = p.next;
                }
            }
            return result;
        }
    }
}

