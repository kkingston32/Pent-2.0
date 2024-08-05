// Pent 2.0. 
//  Created by EL HIRACH ABDERRAZZAK on 2024-02-14. 
//  Copyright © 2024 aelhirach.me. All rights reserved.

using System.Drawing;

namespace Pente
{
	internal sealed class Board
	{
		public Place[,] area;

		public int lastMoveX;

		public int lastMoveY;

		public int _whiteCost;

		public int _blackCost;

		public int WhiteCost
		{
			get
			{
				this._whiteCost = MiniMax.CostBlack(this, Place.White, Place.Black);
				return this._whiteCost;
			}
		}

		public int BlackCost
		{
			get
			{
				this._blackCost = MiniMax.CostBlack(this, Place.Black, Place.White);
				return this._blackCost;
			}
		}

		public Board()
		{
			this.area = new Place[19, 19];
			this.lastMoveX = -1;
			this.lastMoveY = -1;
			this._whiteCost = (this._blackCost = -2147483648);
		}

		public Board(Board origin, int x, int y)
		{
			this.area = (Place[,])origin.area.Clone();
			this.lastMoveX = x;
			this.lastMoveY = y;
			this._whiteCost = (this._blackCost = -2147483648);
		}

		public void init()
		{
			this._whiteCost = (this._blackCost = -2147483648);
		}

		public Rectangle IsGameOver()
		{
			for (int i = 0; i < 19; i++)
			{
				for (int j = 0; j < 19; j++)
				{
					if (this.area[i, j] != Place.Null)
					{
						int num = 1;
						while (num < 5 && j + num < 19 && this.area[i, j] == this.area[i, j + num])
						{
							num++;
						}
						if (num == 5)
						{
							return new Rectangle(i, j, i, j + (num - 1));
						}
						num = 1;
						while (num < 5 && i + num < 19 && this.area[i, j] == this.area[i + num, j])
						{
							num++;
						}
						if (num == 5)
						{
							return new Rectangle(i, j, i + (num - 1), j);
						}
						num = 1;
						while (num < 5 && j + num < 19 && i + num < 19 && this.area[i, j] == this.area[i + num, j + num])
						{
							num++;
						}
						if (num-- == 5)
						{
							return new Rectangle(i, j, i + num, j + num);
						}
						num = 1;
						while (num < 5 && j - num >= 0 && i - num >= 0 && this.area[i, j] == this.area[i - num, j - num])
						{
							num++;
						}
						if (num-- == 5)
						{
							return new Rectangle(i, j, i - num, j - num);
						}
						num = 1;
						while (num < 5 && i + num < 19 && j - num >= 0 && this.area[i, j] == this.area[i + num, j - num])
						{
							num++;
						}
						if (num-- == 5)
						{
							return new Rectangle(i, j, i + num, j - num);
						}
						num = 1;
						while (num < 5 && i - num >= 0 && j + num < 19 && this.area[i, j] == this.area[i - num, j + num])
						{
							num++;
						}
						if (num-- == 5)
						{
							return new Rectangle(i, j, i - num, j + num);
						}
					}
				}
			}
			return Rectangle.Empty;
		}

		public bool IsOver()
		{
			return this.BlackCost >= 100000000 || this.WhiteCost >= 100000000;
		}

		public bool IsNear(int i, int j, int radius)
		{
			for (int k = 0; k <= radius; k++)
			{
				if (i + k < 19 && this.area[i + k, j] != Place.Null)
				{
					return true;
				}
				if (i - k >= 0 && this.area[i - k, j] != Place.Null)
				{
					return true;
				}
				if (j + k < 19 && this.area[i, j + k] != Place.Null)
				{
					return true;
				}
				if (j - k >= 0 && this.area[i, j - k] != Place.Null)
				{
					return true;
				}
				if (j + k < 19 && i + k < 19 && this.area[i + k, j + k] != Place.Null)
				{
					return true;
				}
				if (j - k >= 0 && i + k < 19 && this.area[i + k, j - k] != Place.Null)
				{
					return true;
				}
				if (j + k < 19 && i - k >= 0 && this.area[i - k, j + k] != Place.Null)
				{
					return true;
				}
				if (j - k >= 0 && i - k >= 0 && this.area[i - k, j - k] != Place.Null)
				{
					return true;
				}
			}
			return false;
		}
	}
}
