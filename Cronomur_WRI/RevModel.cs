using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronomur_WRI
{
	public struct RevMsgStruct
	{
		public string sCodeData;

		public int nAntenna;

		public DateTime tBeginTime;

		public DateTime tLastTime;

		public long nRepeatTime;
	}


	public class RevMsgBuffer
	{
		List<RevMsgStruct> revMsgArray = new List<RevMsgStruct>();

		public void RevMsgAdd(RevMsgStruct revMsg)
		{
		/*
			for (int tempi = 0; tempi < revMsgArray.Count; ++tempi)
			{
				if (revMsgArray[tempi].sCodeData == revMsg.sCodeData)
				{
					RevMsgStruct revMsgTemp = revMsgArray[tempi];
					revMsgTemp.nRepeatTime += revMsg.nRepeatTime;
					revMsgTemp.tLastTime = revMsg.tLastTime;
					revMsgArray[tempi] = revMsgTemp;
					return;
				}
			}
			*/

			revMsgArray.Add(revMsg);
		}

		/// <summary>
		/// Adds a RevMsgStruct only once.
		/// </summary>
		/// <param name="msg"></param>
		public void addOnce(RevMsgStruct msg) {
			var t = revMsgArray.Where(tmsg => tmsg.sCodeData == msg.sCodeData);

			if (t.Count() == 0)
				revMsgArray.Add(msg);
		}

		public List<RevMsgStruct> getList() {
			return revMsgArray;
		}

		public void RemoveMsg(string RemoveCodeData)
		{
			int nRemoveNum = 0;
			while (nRemoveNum == revMsgArray.Count)
			{
				if (revMsgArray[nRemoveNum].sCodeData == RemoveCodeData)
					revMsgArray.Remove(revMsgArray[nRemoveNum]);
			}
		}

		public void ClearMsg()
		{
			revMsgArray.Clear();
		}

		public bool RevMsgGet(ref RevMsgStruct revMsg, int nArrayNum)
		{
			if (revMsgArray.Count == 0)
				return false;

			if (nArrayNum >= 0 && nArrayNum < revMsgArray.Count)
			{
				revMsg = revMsgArray[nArrayNum];
				return true;
			}
			return false;
		}

		public RevMsgStruct Get(int i)
		{
			return revMsgArray[i];
		}

		public int TagCount
		{
			get { return revMsgArray.Count; }
		}
	}
}
