// Pent 2.0. 
//  Created by EL HIRACH ABDERRAZZAK on 2024-02-14. 
//  Copyright © 2024 aelhirach.me.  All rights reserved.

using System;
using System.Threading;

namespace Pente
{
	internal static class MiniMax
	{
		public const int depthDeep = 2;
		public const int size = 19;
		public static Board board;
		public static Board BestPlay;
		private static int[,] horizontalB;
		private static int[,] verticalB;

		private static int[,] diagonal1B;

		private static int[,] diagonal2B;

		private static int[,] verticaliA;

		private static int[,] horizontalA;

		private static int[,] diagonal1A;

		private static int[,] diagonal2A;
		public static void WhiteMove(int x, int y)
		{
			MiniMax.board.lastMoveX = x;
			MiniMax.board.lastMoveY = y;
			MiniMax.board.area[x, y] = Place.White;
		}
		public static void Play()
		{
			DateTime now = DateTime.Now;
			MiniMax.BestPlay = null;
            // alpha = -2147483648
            //beta = + 2147483647
			MiniMax.Calculate(0, new State(-2147483648, 2147483647));
			int num = (int)(DateTime.Now - now).TotalMilliseconds;
			num = 100 - num;
			if (num > 0)
			{
				Thread.Sleep(num);
			}
		}
		private static State Calculate(int depth, State current)
		{
			if (depth < 2 && !MiniMax.board.IsOver())
			{
				for (int i = 0; i < 19; i++)
				{
					for (int j = 0; j < 19; j++)
					{
						if (MiniMax.IsInside(i, j, depth) && MiniMax.board.area[i, j] == Place.Null)
						{
							if (depth % 2 == 0)
							{
								MiniMax.board.area[i, j] = Place.Black;
							}
							else
							{
								MiniMax.board.area[i, j] = Place.White;
							}
							State state = MiniMax.Calculate(depth + 1, current);
							if (depth % 2 == 0)
							{
								if (current.alpha < state.beta)
								{
									current.alpha = state.beta;
									if (depth == 0)
									{
										MiniMax.BestPlay = new Board(MiniMax.board, i, j);
									}
								}
							}
							else if (current.beta > state.alpha)
							{
								current.beta = state.alpha;
							}
							MiniMax.board.area[i, j] = Place.Null;
							if (current.alpha >= current.beta)
							{
								return current;
							}
						}
					}
				}
				for (int k = 0; k < 19; k++)
				{
					for (int l = 0; l < 19; l++)
					{
						if (!MiniMax.IsInside(k, l, depth) && MiniMax.board.area[k, l] == Place.Null)
						{
							if (depth % 2 == 0)
							{
								MiniMax.board.area[k, l] = Place.Black;
							}
							else
							{
								MiniMax.board.area[k, l] = Place.White;
							}
							State state2 = MiniMax.Calculate(depth + 1, current);
							if (depth % 2 == 0)
							{
								if (current.alpha < state2.beta)
								{
									current.alpha = state2.beta;
									if (depth == 0)
									{
										MiniMax.BestPlay = new Board(MiniMax.board, k, l);
									}
								}
							}
							else if (current.beta > state2.alpha)
							{
								current.beta = state2.alpha;
							}
							MiniMax.board.area[k, l] = Place.Null;
							if (current.alpha >= current.beta)
							{
								return current;
							}
						}
					}
				}
			}
			else if (depth % 2 == 0)
			{
				current.alpha = MiniMax.StateCost(MiniMax.board);
			}
			else
			{
				current.beta = MiniMax.StateCost(MiniMax.board);
			}
			return current;
		}
		private static bool IsInside(int i, int j, int depth)
		{
			if (i > 0)
			{
				if (MiniMax.board.area[i - 1, j] != Place.Null)
				{
					return true;
				}
				if (j > 0 && MiniMax.board.area[i - 1, j - 1] != Place.Null)
				{
					return true;
				}
				if (j < 18 && MiniMax.board.area[i - 1, j + 1] != Place.Null)
				{
					return true;
				}
			}
			if (i < 18)
			{
				if (MiniMax.board.area[i + 1, j] != Place.Null)
				{
					return true;
				}
				if (j > 0 && MiniMax.board.area[i + 1, j - 1] != Place.Null)
				{
					return true;
				}
				if (j < 18 && MiniMax.board.area[i + 1, j + 1] != Place.Null)
				{
					return true;
				}
			}
			return (j < 18 && MiniMax.board.area[i, j + 1] != Place.Null) || (j > 0 && MiniMax.board.area[i, j - 1] != Place.Null);
		}
		private static int StateCost(Board current)
		{
			return current.BlackCost - current.WhiteCost;
		}
		public static void NewCost(Board current)
		{
			if (MiniMax.horizontalA == null)
			{
				MiniMax.horizontalB = new int[15, 19];
				MiniMax.verticalB = new int[19, 15];
				MiniMax.diagonal1B = new int[15, 15];
				MiniMax.diagonal2B = new int[15, 15];
				MiniMax.horizontalA = new int[15, 19];
				MiniMax.verticaliA = new int[19, 15];
				MiniMax.diagonal1A = new int[15, 15];
				MiniMax.diagonal2A = new int[15, 15];
			}
			for (int i = 0; i < 19; i++)
			{
				for (int j = 0; j < 19; j++)
				{
					switch (current.area[i, j])
					{
					case Place.Black:
						for (int k = -4; k <= 0; k++)
						{
							MiniMax.Inc(MiniMax.verticalB, i - k, j);
							MiniMax.Inc(MiniMax.horizontalB, i, j - k);
							MiniMax.Inc(MiniMax.diagonal1B, i - k, j - k);
							MiniMax.Inc(MiniMax.diagonal2B, i - k, j + k);
							MiniMax.Block(MiniMax.verticaliA, i - k, j);
							MiniMax.Block(MiniMax.horizontalA, i, j - k);
							MiniMax.Block(MiniMax.diagonal1A, i - k, j - k);
							MiniMax.Block(MiniMax.diagonal2A, i - k, j + k);
						}
						break;
					case Place.White:
						for (int l = -4; l <= 0; l++)
						{
							MiniMax.Block(MiniMax.verticalB, i - l, j);
							MiniMax.Block(MiniMax.horizontalB, i, j - l);
							MiniMax.Block(MiniMax.diagonal1B, i - l, j - l);
							MiniMax.Block(MiniMax.diagonal2B, i - l, j + l);
							MiniMax.Inc(MiniMax.verticaliA, i - l, j);
							MiniMax.Inc(MiniMax.horizontalA, i, j - l);
							MiniMax.Inc(MiniMax.diagonal1A, i - l, j - l);
							MiniMax.Inc(MiniMax.diagonal2A, i - l, j + l);
						}
						break;
					}
				}
			}
			MiniMax.board._blackCost = MiniMax.Count(MiniMax.verticalB) + MiniMax.Count(MiniMax.horizontalB) + MiniMax.Count(MiniMax.diagonal1B) + MiniMax.Count(MiniMax.diagonal2B);
			MiniMax.board._whiteCost = MiniMax.Count(MiniMax.verticaliA) + MiniMax.Count(MiniMax.horizontalA) + MiniMax.Count(MiniMax.diagonal1A) + MiniMax.Count(MiniMax.diagonal2A);
		}
		public static void Inc(int[,] array, int i, int j)
		{
			if (i >= 0 && i < array.GetLength(0) && j >= 0 && j < array.GetLength(1) && array[i, j] != -1)
			{
				array[i, j]++;
			}
		}

		public static void Block(int[,] array, int i, int j)
		{
			if (i >= 0 && i < array.GetLength(0) && j >= 0 && j < array.GetLength(1) && array[i, j] != -1)
			{
				array[i, j] = -1;
			}
		}

		public static int Count(int[,] array)
		{
			int num = 0;
			for (int i = 0; i < array.GetLength(0); i++)
			{
				for (int j = 0; j < array.GetLength(1); j++)
				{
					switch (array[i, j])
					{
					case 1:
						num++;
						break;
					case 2:
						num += 100;
						break;
					case 3:
						num += 10000;
						break;
					case 4:
						num += 1000000;
						break;
					case 5:
						num += 100000000;
						break;
					}
					array[i, j] = 0;
				}
			}
			return num;
		}

		public static int CostBlack(Board current, Place Black, Place White)
		{
			int num = 0;
			for (int i = 0; i < 19; i++)
			{
				int num2 = 0;
				int num3 = 0;
				for (int j = 0; j < 19; j++)
				{
					num3++;
					if (current.area[i, j] == Black)
					{
						num2++;
					}
					else if (current.area[i, j] == White)
					{
						num3 = 0;
						num2 = 0;
					}
					if (num3 == 5)
					{
						num3--;
						switch (num2)
						{
						case 1:
							num++;
							break;
						case 2:
							num += 100;
							break;
						case 3:
							num += 10000;
							break;
						case 4:
							num += 1000000;
							break;
						case 5:
							num += 100000000;
							break;
						}
						if (current.area[i, j - 4] == Black)
						{
							num2--;
						}
					}
				}
			}
			for (int k = 0; k < 19; k++)
			{
				int num4 = 0;
				int num5 = 0;
				for (int l = 0; l < 19; l++)
				{
					num5++;
					if (current.area[l, k] == Black)
					{
						num4++;
					}
					else if (current.area[l, k] == White)
					{
						num5 = 0;
						num4 = 0;
					}
					if (num5 == 5)
					{
						num5--;
						switch (num4)
						{
						case 1:
							num++;
							break;
						case 2:
							num += 100;
							break;
						case 3:
							num += 10000;
							break;
						case 4:
							num += 1000000;
							break;
						case 5:
							num += 100000000;
							break;
						}
						if (current.area[l - 4, k] == Black)
						{
							num4--;
						}
					}
				}
			}
			for (int m = 0; m < 38; m++)
			{
				int num6 = 0;
				int num7 = 0;
				for (int n = 0; n < 19; n++)
				{
					if (m - n < 19 && m - n >= 0)
					{
						num7++;
						if (current.area[m - n, n] == Black)
						{
							num6++;
						}
						else if (current.area[m - n, n] == White)
						{
							num7 = 0;
							num6 = 0;
						}
						if (num7 == 5)
						{
							num7--;
							switch (num6)
							{
							case 1:
								num++;
								break;
							case 2:
								num += 100;
								break;
							case 3:
								num += 10000;
								break;
							case 4:
								num += 1000000;
								break;
							case 5:
								num += 100000000;
								break;
							}
							if (current.area[m - n + 4, n - 4] == Black)
							{
								num6--;
							}
						}
					}
				}
			}
			for (int num8 = -19; num8 < 19; num8++)
			{
				int num9 = 0;
				int num10 = 0;
				for (int num11 = 0; num11 < 19; num11++)
				{
					if (num8 + num11 < 19 && num8 + num11 >= 0)
					{
						num10++;
						if (current.area[num8 + num11, num11] == Black)
						{
							num9++;
						}
						else if (current.area[num8 + num11, num11] == White)
						{
							num10 = 0;
							num9 = 0;
						}
						if (num10 == 5)
						{
							num10--;
							switch (num9)
							{
							case 1:
								num++;
								break;
							case 2:
								num += 100;
								break;
							case 3:
								num += 10000;
								break;
							case 4:
								num += 1000000;
								break;
							case 5:
								num += 100000000;
								break;
							}
							if (current.area[num8 + num11 - 4, num11 - 4] == Black)
							{
								num9--;
							}
						}
					}
				}
			}
			return num;
		}
	}
}
