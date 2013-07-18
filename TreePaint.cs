using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TreeLayoutHelper;
using TreeLayoutHelper.FlowLayout;
using TreeLayoutHelper.HierarchyLayout;
using TreeLayoutHelper.TreeLayout;

namespace WindowsFormsApplication1
{
    public class JFlowTransformer : IFlowNodeTransformer<TreeNode>
    {
        public ElementType ClassifyNode(TreeNode node)
        {
            return ElementType.Node;
        }
        public void AddJunctionNodes(TreeNode parallelNode, ref bool junctionBefore, ref bool junctionAfter)
        {

        }

        public bool HasNodeFrame(TreeNode node)
        {
            return true;
        }
    }
    public class TreeNodeNavigator : ITreeNavigator<TreeNode>
    {
        public TreeNode GetParent(TreeNode node)
        {
            return node.Parent;
        }

        public int GetChildCount(TreeNode node)
        {
            return node.Nodes.Count;
        }

        public TreeNode GetChild(TreeNode node, int index)
        {
            return node.Nodes[index];
        }
    }

    public class TreePaint
    {

        public void SetOrientation(TreeLayoutHelper.Orientation orientation)
        {
            treeModel.Orientation = orientation;
            flowModel.Orientation = orientation;
            hierarchyModel.Orientation = orientation;
        }
        public TreePaint(TreeNode root)
        {
            this.Root = root;

            var navigator = new TreeNodeNavigator();

            {
                var treeTransformer = new TreeLayoutTransformer<TreeNode>(navigator, new JFlowTransformer());
                this.treeModel = treeTransformer.TransformTree(root);
            }

            {
                var flowTransformer = new FlowLayoutTransformer<TreeNode>(navigator, new JFlowTransformer());
                this.flowModel = flowTransformer.TransformTree(root);
            }

            {
                var hierarchyTransformer = new HierarchyLayoutTransformer<TreeNode>(navigator, new JFlowTransformer());
                this.hierarchyModel = hierarchyTransformer.TransformTree(root);
                this.hierarchyModel.LevelIndentation = 20;
            }

        }

        public TreeNode Root { get; set; }

        private readonly TreeLayoutModel<TreeNode> treeModel;

        public TreeLayoutModel<TreeNode> TreeModel
        {
            get
            {
                return treeModel;
            }
        }

        private readonly FlowLayoutModel<TreeNode> flowModel;

        public FlowLayoutModel<TreeNode> FlowModel
        {
            get
            {
                return flowModel;
            }
        }

        private readonly HierarchyLayoutModel<TreeNode> hierarchyModel;

        public HierarchyLayoutModel<TreeNode> HierarchyModel
        {
            get
            {
                return hierarchyModel;
            }
        }

        #region GDI+ drawing
        public void DrawTree(System.Drawing.Graphics g)
        {
            var canvas = new MultiPurposeCanvas(g);

            this.TreeModel.Canvas = canvas;
            canvas.InitializeDrawing(this.TreeModel);
            this.TreeModel.Draw();
            canvas.FinalizeDrawing(this.TreeModel);
        }

        public void DrawFlow(System.Drawing.Graphics g)
        {
            var canvas = new MultiPurposeCanvas(g);

            this.FlowModel.Canvas = canvas;
            canvas.InitializeDrawing(this.FlowModel);
            this.FlowModel.Draw();
            canvas.FinalizeDrawing(this.FlowModel);
        }

        public void DrawHierarchy(System.Drawing.Graphics g)
        {
            var canvas = new MultiPurposeCanvas(g);

            this.HierarchyModel.Canvas = canvas;
            canvas.InitializeDrawing(this.HierarchyModel);
            this.HierarchyModel.Draw();
            canvas.FinalizeDrawing(this.HierarchyModel);
        }
        #endregion
    }

    public class MultiPurposeCanvas : ITreeCanvas<TreeNode>, IFlowCanvas<TreeNode>, IHierarchyCanvas<TreeNode>
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="graphics">The target graphics context.</param>
        /// <exception cref="ArgumentNullException"><paramref name="graphics"/> is <see langword="null"/>.</exception>
        public MultiPurposeCanvas(Graphics graphics)
        {
            if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }

            this.graphics = graphics;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
        }

        /// <summary>
        /// The target graphics context.
        /// </summary>
        private readonly Graphics graphics;

        /// <summary>
        /// The formatting used for strings.
        /// </summary>
        private static StringFormat format = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        #region measuring
        /// <summary>
        /// Retrieves the size of a single node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="width">Receives the width of the node.</param>
        /// <param name="height">Receives the height of the node.</param>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is <see langword="null"/>.</exception>
        public void MeasureNode(TreeNode node, out int width, out int height)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            var size = graphics.MeasureString(node.Text, SystemFonts.DefaultFont);
            width = Convert.ToInt32(size.Width) + 6;
            height = Convert.ToInt32(size.Height) + 6;
        }

        /// <summary>
        /// Measures the frame of a node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="subtreeWidth">The width of the complete subtree with <paramref name="node"/> as the root node.</param>
        /// <param name="subtreeHeight">The height of the complete subtree with <paramref name="node"/> as the root node.</param>
        /// <param name="left">The additional space for the frame on the left side.</param>
        /// <param name="top">The additional space for the frame on the upper side.</param>
        /// <param name="right">The additional space for the frame on the right side.</param>
        /// <param name="bottom">The additional space for the frame on the lower side.</param>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> or <paramref name="node"/> is <see langword="null"/>.</exception>
        public void MeasureFrame(TreeNode node, int subtreeWidth, int subtreeHeight, ref int left, ref int top, ref int right, ref int bottom)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            left = top = right = bottom = 7;
        }
        #endregion

        #region drawing
        private const int NodeCornerRadius = 5;

        private GraphicsPath CreateRoundRect(int left, int top, int width, int height, int cornerRadius)
        {
            var gp = new GraphicsPath();
            gp.AddBezier(left + cornerRadius, top, left, top, left, top, left, top + cornerRadius);
            gp.AddLine(left, top + cornerRadius, left, top + height - cornerRadius);
            gp.AddBezier(left, top + height - cornerRadius, left, top + height, left, top + height, left + cornerRadius, top + height);
            gp.AddLine(left + cornerRadius, top + height, left + width - cornerRadius, top + height);
            gp.AddBezier(left + width - cornerRadius, top + height, left + width, top + height, left + width, top + height, left + width, top + height - cornerRadius);
            gp.AddLine(left + width, top + height - cornerRadius, left + width, top + cornerRadius);
            gp.AddBezier(left + width, top + cornerRadius, left + width, top, left + width, top, left + width - cornerRadius, top);
            gp.CloseFigure();
            return gp;
        }

        /// <summary>
        /// Draws a node.
        /// </summary>
        /// <param name="node">The node to draw.</param>
        public void DrawNode(NodeWithBounds<TreeNode> node)
        {
            var evalResult = node.Node.EvaluationResult();
            Color frameCol;
            if (evalResult.HasValue)
            {
                if (evalResult.Value)
                {
                    frameCol = Color.Green;
                }
                else
                {
                    frameCol = Color.Red;
                }
            }
            else
            {
                frameCol = Color.Navy;
            }

            using (var gp = CreateRoundRect(node.X, node.Y, node.Width, node.Height, NodeCornerRadius))
            {
                using (var b = new LinearGradientBrush(new Rectangle(node.X, node.Y, node.Width, node.Height),
                                                       Color.AliceBlue, Color.LightBlue,
                                                       LinearGradientMode.ForwardDiagonal))
                {
                    graphics.FillPath(b, gp);
                }
                using (var p = new Pen(frameCol, 2))
                {
                    graphics.DrawPath(p, gp);
                }
            }
            graphics.DrawString(node.Node.Text, SystemFonts.DefaultFont, Brushes.Black,
                                new RectangleF(node.X, node.Y, node.Width, node.Height),
                                format);
        }

        /// <summary>
        /// Draws the connections between a given parent node and all of its immediate child nodes.
        /// </summary>
        /// <param name="model">The tree model.</param>
        /// <param name="parent">The parent node.</param>
        /// <param name="children">The child nodes.</param>
        /// <exception cref="ArgumentNullException">Any of the arguments is <see langword="null"/>.</exception>
        public void DrawTreeConnections(TreeLayoutModel<TreeNode> model, NodeWithBounds<TreeNode> parent, NodeWithBounds<TreeNode>[] children)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }
            if (children == null)
            {
                throw new ArgumentNullException("children");
            }

            switch (model.Orientation)
            {
                case TreeLayoutHelper.Orientation.LeftToRight:
                    {
                        Point startPt = new Point(parent.Right, parent.Y + parent.Height / 2);
                        foreach (var child in children)
                        {
                            graphics.DrawLine(Pens.Navy, startPt.X, startPt.Y, child.X, child.Y + child.Height / 2);
                        }
                    }
                    break;
                case TreeLayoutHelper.Orientation.TopToBottom:
                    {
                        Point startPt = new Point(parent.X + parent.Width / 2, parent.Bottom);
                        foreach (var child in children)
                        {
                            graphics.DrawLine(Pens.Navy, startPt.X, startPt.Y, child.X + child.Width / 2, child.Y);
                        }
                    }
                    break;
                case TreeLayoutHelper.Orientation.RightToLeft:
                    {
                        Point startPt = new Point(parent.X, parent.Y + parent.Height / 2);
                        foreach (var child in children)
                        {
                            graphics.DrawLine(Pens.Navy, startPt.X, startPt.Y, child.Right, child.Y + child.Height / 2);
                        }
                    }
                    break;
                case TreeLayoutHelper.Orientation.BottomToTop:
                    {
                        Point startPt = new Point(parent.X + parent.Width / 2, parent.Y);
                        foreach (var child in children)
                        {
                            graphics.DrawLine(Pens.Navy, startPt.X, startPt.Y, child.X + child.Width / 2, child.Bottom);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Draws a frame.
        /// </summary>
        /// <param name="frameNode">The node that required the frame.</param>
        /// <param name="x">The horizontal offset of the frame.</param>
        /// <param name="y">The vertical offset of the frame.</param>
        /// <param name="width">The width of the frame.</param>
        /// <param name="height">The height of the frame.</param>
        /// <param name="nestedFrames">The number of nested frames.</param>
        /// <exception cref="ArgumentNullException"><paramref name="frameNode"/> is <see langword="null"/>.</exception>
        public void DrawFrame(TreeNode frameNode, int x, int y, int width, int height, int nestedFrames)
        {
            if (frameNode == null)
            {
                throw new ArgumentNullException("frameNode");
            }

            using (var b = new LinearGradientBrush(new Rectangle(x, y, width, height),
                                                   Color.White, nestedFrames % 2 == 0 ? Color.LightGreen : Color.PaleGoldenrod,
                                                   LinearGradientMode.ForwardDiagonal))
            {
                graphics.FillRectangle(b, x, y, width, height);
            }
            graphics.DrawRectangle(nestedFrames % 2 == 0 ? Pens.Green : Pens.Goldenrod, x, y, width, height);
        }

        /// <summary>
        /// Draws one or more connection lines between several nodes.
        /// </summary>
        /// <param name="model">The flow graph model.</param>
        /// <param name="sources">The source nodes.</param>
        /// <param name="destinations">The destination nodes.</param>
        /// <param name="connection">The node that represents the connection.</param>
        /// <exception cref="ArgumentNullException">Any of the arguments is <see langword="null"/>.</exception>
        /// <remarks>
        /// <para>This method draws connections defined by a sequence node.
        ///   Usually, this will be exactly one connection line between two nodes.
        ///   However, there are cases where either there is more than one source node or more than one destination node.
        ///   A very special but nonetheless possible case is when there are several source nodes and several destination nodes at the same time.
        ///   The same applies to the situation that the set of source nodes or the set of destination nodes is empty, though this should not normally occur in a properly structured graph.</para>
        /// </remarks>
        public void DrawFlowConnections(FlowLayoutModel<TreeNode> model, NodeWithBounds<TreeNode>[] sources, NodeWithBounds<TreeNode>[] destinations, TreeNode connection)
        {
            if (sources == null)
            {
                throw new ArgumentNullException("sources");
            }
            if (destinations == null)
            {
                throw new ArgumentNullException("destinations");
            }

            if ((sources.Length <= 0) || (destinations.Length <= 0))
            {
                return;
            }

            if ((sources.Length == 1) && (destinations.Length == 1))
            {
                var src = sources[0];
                var dest = destinations[0];

                var flowPen = GetFlowPen(connection, src);

                switch (model.Orientation)
                {
                    case TreeLayoutHelper.Orientation.LeftToRight:
                        {
                            int startY = src.Y + src.Height / 2;
                            int endY = dest.Y + dest.Height / 2;
                            int middleX = src.Right + (dest.X - src.Right) / 2;

                            graphics.DrawLines(flowPen, new Point[] {
							                   	new Point(src.Right, startY),
							                   	new Point(middleX, startY),
							                   	new Point(middleX, endY),
							                   	new Point(dest.X, endY)
							                   });
                        }
                        break;
                    case TreeLayoutHelper.Orientation.TopToBottom:
                        {
                            int startX = src.X + src.Width / 2;
                            int endX = dest.X + dest.Width / 2;
                            int middleY = src.Bottom + (dest.Y - src.Bottom) / 2;

                            graphics.DrawLines(flowPen, new Point[] {
							                   	new Point(startX, src.Bottom),
							                   	new Point(startX, middleY),
							                   	new Point(endX, middleY),
							                   	new Point(endX, dest.Y)
							                   });
                        }
                        break;
                    case TreeLayoutHelper.Orientation.RightToLeft:
                        {
                            int startY = src.Y + src.Height / 2;
                            int endY = dest.Y + dest.Height / 2;
                            int middleX = dest.Right + (src.X - dest.Right) / 2;

                            graphics.DrawLines(flowPen, new Point[] {
							                   	new Point(src.X, startY),
							                   	new Point(middleX, startY),
							                   	new Point(middleX, endY),
							                   	new Point(dest.Right, endY)
							                   });
                        }
                        break;
                    case TreeLayoutHelper.Orientation.BottomToTop:
                        {
                            int startX = src.X + src.Width / 2;
                            int endX = dest.X + dest.Width / 2;
                            int middleY = dest.Bottom + (src.Y - dest.Bottom) / 2;

                            graphics.DrawLines(flowPen, new Point[] {
							                   	new Point(startX, src.Y),
							                   	new Point(startX, middleY),
							                   	new Point(endX, middleY),
							                   	new Point(endX, dest.Bottom)
							                   });
                        }
                        break;
                }
            }
            else
            {
                Pen commonFlowPen = GetFlowPen(connection, sources);

                switch (model.Orientation)
                {
                    case TreeLayoutHelper.Orientation.LeftToRight:
                        {
                            int rightmostSrc = sources.Max(src => src.Right);
                            int leftmostDest = destinations.Min(dest => dest.X);
                            int middleX1 = rightmostSrc + (leftmostDest - rightmostSrc) / 3;
                            int middleX2 = rightmostSrc + (leftmostDest - rightmostSrc) * 2 / 3;
                            int middleY = (sources.Length == 1 ? sources[0].Y + sources[0].Height / 2
                                           : (destinations.Length == 1 ? destinations[0].Y + destinations[0].Height / 2
                                              : Convert.ToInt32((sources.Average(src => src.Y + src.Height / 2) + destinations.Average(dest => dest.Y + dest.Height / 2)) / 2)));

                            foreach (var src in sources)
                            {
                                int startY = src.Y + src.Height / 2;

                                graphics.DrawLines(GetFlowPen(connection, src), new Point[] {
								                   	new Point(src.Right, startY),
								                   	new Point(middleX1, startY),
								                   	new Point(middleX1, middleY)
								                   });

                                foreach (var dest in destinations)
                                {
                                    int endY = dest.Y + dest.Height / 2;

                                    graphics.DrawLines(commonFlowPen, new Point[] {
									                   	new Point(middleX1, middleY),
									                   	new Point(middleX2, middleY),
									                   	new Point(middleX2, endY),
									                   	new Point(dest.X, endY)
									                   });
                                }
                            }
                        }
                        break;
                    case TreeLayoutHelper.Orientation.TopToBottom:
                        {
                            int bottommostSrc = sources.Max(src => src.Bottom);
                            int topmostDest = destinations.Min(dest => dest.Y);
                            int middleY1 = bottommostSrc + (topmostDest - bottommostSrc) / 3;
                            int middleY2 = bottommostSrc + (topmostDest - bottommostSrc) * 2 / 3;
                            int middleX = (sources.Length == 1 ? sources[0].X + sources[0].Width / 2
                                           : (destinations.Length == 1 ? destinations[0].X + destinations[0].Width / 2
                                              : Convert.ToInt32((sources.Average(src => src.X + src.Width / 2) + destinations.Average(dest => dest.X + dest.Width / 2)) / 2)));

                            foreach (var src in sources)
                            {
                                int startX = src.X + src.Width / 2;

                                graphics.DrawLines(GetFlowPen(connection, src), new Point[] {
								                   	new Point(startX, src.Bottom),
								                   	new Point(startX, middleY1),
								                   	new Point(middleX, middleY1)
								                   });

                                foreach (var dest in destinations)
                                {
                                    int endX = dest.X + dest.Width / 2;

                                    graphics.DrawLines(commonFlowPen, new Point[] {
									                   	new Point(middleX, middleY1),
									                   	new Point(middleX, middleY2),
									                   	new Point(endX, middleY2),
									                   	new Point(endX, dest.Y)
									                   });
                                }
                            }
                        }
                        break;
                    case TreeLayoutHelper.Orientation.RightToLeft:
                        {
                            int leftmostSrc = sources.Min(src => src.X);
                            int rightmostDest = destinations.Max(dest => dest.Right);
                            int middleX1 = leftmostSrc - (leftmostSrc - rightmostDest) / 3;
                            int middleX2 = leftmostSrc - (leftmostSrc - rightmostDest) * 2 / 3;
                            int middleY = (sources.Length == 1 ? sources[0].Y + sources[0].Height / 2
                                           : (destinations.Length == 1 ? destinations[0].Y + destinations[0].Height / 2
                                              : Convert.ToInt32((sources.Average(src => src.Y + src.Height / 2) + destinations.Average(dest => dest.Y + dest.Height / 2)) / 2)));

                            foreach (var src in sources)
                            {
                                int startY = src.Y + src.Height / 2;

                                graphics.DrawLines(GetFlowPen(connection, src), new Point[] {
								                   	new Point(src.X, startY),
								                   	new Point(middleX1, startY),
								                   	new Point(middleX1, middleY)
								                   });

                                foreach (var dest in destinations)
                                {
                                    int endY = dest.Y + dest.Height / 2;

                                    graphics.DrawLines(commonFlowPen, new Point[] {
									                   	new Point(middleX1, middleY),
									                   	new Point(middleX2, middleY),
									                   	new Point(middleX2, endY),
									                   	new Point(dest.Right, endY)
									                   });
                                }
                            }
                        }
                        break;
                    case TreeLayoutHelper.Orientation.BottomToTop:
                        {
                            int topmostSrc = sources.Min(src => src.Y);
                            int bottommostDest = destinations.Max(dest => dest.Bottom);
                            int middleY1 = topmostSrc - (topmostSrc - bottommostDest) / 3;
                            int middleY2 = topmostSrc - (topmostSrc - bottommostDest) * 2 / 3;
                            int middleX = (sources.Length == 1 ? sources[0].X + sources[0].Width / 2
                                           : (destinations.Length == 1 ? destinations[0].X + destinations[0].Width / 2
                                              : Convert.ToInt32((sources.Average(src => src.X + src.Width / 2) + destinations.Average(dest => dest.X + dest.Width / 2)) / 2)));

                            foreach (var src in sources)
                            {
                                int startX = src.X + src.Width / 2;

                                graphics.DrawLines(GetFlowPen(connection, src), new Point[] {
								                   	new Point(startX, src.Y),
								                   	new Point(startX, middleY1),
								                   	new Point(middleX, middleY1)
								                   });

                                foreach (var dest in destinations)
                                {
                                    int endX = dest.X + dest.Width / 2;

                                    graphics.DrawLines(commonFlowPen, new Point[] {
									                   	new Point(middleX, middleY1),
									                   	new Point(middleX, middleY2),
									                   	new Point(endX, middleY2),
									                   	new Point(endX, dest.Bottom)
									                   });
                                }
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Draws one or more connection lines at the start of the graph to the first nodes.
        /// </summary>
        /// <param name="model">The flow graph model.</param>
        /// <param name="firstDestinations">The first destination nodes.</param>
        /// <param name="startOffset">The offset along the flow direction of the starting point.</param>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> or <paramref name="firstDestinations"/> is <see langword="null"/>.</exception>
        public void DrawFlowStart(FlowLayoutModel<TreeNode> model, NodeWithBounds<TreeNode>[] firstDestinations, int startOffset)
        {
            if (firstDestinations == null)
            {
                throw new ArgumentNullException("firstDestinations");
            }

            if (firstDestinations.Length > 0)
            {
                Pen flowPen = firstDestinations[0].Node.FlowResult().HasValue ? FlowTrue : FlowUnknown;

                switch (model.Orientation)
                {
                    case TreeLayoutHelper.Orientation.LeftToRight:
                        {
                            int startY = Convert.ToInt32(firstDestinations.Average(dest => dest.Y + dest.Height / 2));
                            int middleX = startOffset + (firstDestinations.Min(dest => dest.X) - startOffset) / 2;

                            foreach (var dest in firstDestinations)
                            {
                                graphics.DrawLines(flowPen, new Point[] {
								                   	new Point(startOffset, startY),
								                   	new Point(middleX, startY),
								                   	new Point(middleX, dest.Y + dest.Height / 2),
								                   	new Point(dest.X, dest.Y + dest.Height / 2)
								                   });
                            }

                            int arrowSize = Math.Min(7, middleX);
                            DrawFlowArrow(model, new Point(startOffset + arrowSize, startY), arrowSize);
                        }
                        break;
                    case TreeLayoutHelper.Orientation.TopToBottom:
                        {
                            int startX = Convert.ToInt32(firstDestinations.Average(dest => dest.X + dest.Width / 2));
                            int middleY = startOffset + (firstDestinations.Min(dest => dest.Y) - startOffset) / 2;

                            foreach (var dest in firstDestinations)
                            {
                                graphics.DrawLines(flowPen, new Point[] {
								                   	new Point(startX, startOffset),
								                   	new Point(startX, middleY),
								                   	new Point(dest.X + dest.Width / 2, middleY),
								                   	new Point(dest.X + dest.Width / 2, dest.Y)
								                   });
                            }

                            int arrowSize = Math.Min(7, middleY);
                            DrawFlowArrow(model, new Point(startX, startOffset + arrowSize), arrowSize);
                        }
                        break;
                    case TreeLayoutHelper.Orientation.RightToLeft:
                        {
                            int startY = Convert.ToInt32(firstDestinations.Average(dest => dest.Y + dest.Height / 2));
                            int middleX = startOffset - (startOffset - firstDestinations.Max(dest => dest.Right)) / 2;

                            foreach (var dest in firstDestinations)
                            {
                                graphics.DrawLines(flowPen, new Point[] {
								                   	new Point(startOffset, startY),
								                   	new Point(middleX, startY),
								                   	new Point(middleX, dest.Y + dest.Height / 2),
								                   	new Point(dest.Right, dest.Y + dest.Height / 2)
								                   });
                            }

                            int arrowSize = Math.Min(7, middleX);
                            DrawFlowArrow(model, new Point(startOffset - arrowSize, startY), arrowSize);
                        }
                        break;
                    case TreeLayoutHelper.Orientation.BottomToTop:
                        {
                            int startX = Convert.ToInt32(firstDestinations.Average(dest => dest.X + dest.Width / 2));
                            int middleY = startOffset - (startOffset - firstDestinations.Max(dest => dest.Bottom)) / 2;

                            foreach (var dest in firstDestinations)
                            {
                                graphics.DrawLines(flowPen, new Point[] {
								                   	new Point(startX, startOffset),
								                   	new Point(startX, middleY),
								                   	new Point(dest.X + dest.Width / 2, middleY),
								                   	new Point(dest.X + dest.Width / 2, dest.Bottom)
								                   });
                            }

                            int arrowSize = Math.Min(7, middleY);
                            DrawFlowArrow(model, new Point(startX, startOffset - arrowSize), arrowSize);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Draws one or more connection lines at the end of the graph starting at the last nodes.
        /// </summary>
        /// <param name="model">The flow graph model.</param>
        /// <param name="lastSources">The last source nodes.</param>
        /// <param name="endOffset">The offset along the flow direction of the end point.</param>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> or <paramref name="lastSources"/> is <see langword="null"/>.</exception>
        public void DrawFlowEnd(FlowLayoutModel<TreeNode> model, NodeWithBounds<TreeNode>[] lastSources, int endOffset)
        {
            if (lastSources == null)
            {
                throw new ArgumentNullException("lastSources");
            }

            if (lastSources.Length > 0)
            {
                Point endStartPt = new Point(0, 0);
                Point endEndPt = new Point(0, 0);

                switch (model.Orientation)
                {
                    case TreeLayoutHelper.Orientation.LeftToRight:
                        {
                            int endY = Convert.ToInt32(lastSources.Average(src => src.Y + src.Height / 2));

                            int rightmost = lastSources.Max(src => src.Right);
                            int middleX = rightmost + (endOffset - rightmost) / 2;

                            foreach (var src in lastSources)
                            {
                                graphics.DrawLines(GetFlowPen(null, src), new Point[] {
								                   	new Point(src.Right, src.Y + src.Height / 2),
								                   	new Point(middleX, src.Y + src.Height / 2),
								                   	new Point(middleX, endY)
								                   });
                            }

                            endStartPt = new Point(middleX, endY);
                            endEndPt = new Point(endOffset, endY);

                            int arrowSize = Math.Min(7, middleX);
                            DrawFlowArrow(model, new Point(endOffset, endY), arrowSize);
                        }
                        break;
                    case TreeLayoutHelper.Orientation.TopToBottom:
                        {
                            int endX = Convert.ToInt32(lastSources.Average(src => src.X + src.Width / 2));

                            int bottommost = lastSources.Max(src => src.Bottom);
                            int middleY = bottommost + (endOffset - bottommost) / 2;

                            foreach (var src in lastSources)
                            {
                                graphics.DrawLines(GetFlowPen(null, src), new Point[] {
								                   	new Point(src.X + src.Width / 2, src.Bottom),
								                   	new Point(src.X + src.Width / 2, middleY),
								                   	new Point(endX, middleY)
								                   });
                            }

                            endStartPt = new Point(endX, middleY);
                            endEndPt = new Point(endX, endOffset);

                            int arrowSize = Math.Min(7, middleY);
                            DrawFlowArrow(model, new Point(endX, endOffset), arrowSize);
                        }
                        break;
                    case TreeLayoutHelper.Orientation.RightToLeft:
                        {
                            int endY = Convert.ToInt32(lastSources.Average(src => src.Y + src.Height / 2));

                            int leftmost = lastSources.Min(src => src.X);
                            int middleX = endOffset + (leftmost - endOffset) / 2;

                            foreach (var src in lastSources)
                            {
                                graphics.DrawLines(GetFlowPen(null, src), new Point[] {
								                   	new Point(src.X, src.Y + src.Height / 2),
								                   	new Point(middleX, src.Y + src.Height / 2),
								                   	new Point(middleX, endY)
								                   });
                            }

                            endStartPt = new Point(middleX, endY);
                            endEndPt = new Point(endOffset, endY);

                            int arrowSize = Math.Min(7, middleX);
                            DrawFlowArrow(model, new Point(endOffset, endY), arrowSize);
                        }
                        break;
                    case TreeLayoutHelper.Orientation.BottomToTop:
                        {
                            int endX = Convert.ToInt32(lastSources.Average(src => src.X + src.Width / 2));

                            int topmost = lastSources.Min(src => src.Y);
                            int middleY = endOffset + (topmost - endOffset) / 2;

                            foreach (var src in lastSources)
                            {
                                graphics.DrawLines(GetFlowPen(null, src), new Point[] {
								                   	new Point(src.X + src.Width / 2, src.Y),
								                   	new Point(src.X + src.Width / 2, middleY),
								                   	new Point(endX, middleY)
								                   });
                            }

                            endStartPt = new Point(endX, middleY);
                            endEndPt = new Point(endX, endOffset);

                            int arrowSize = Math.Min(7, middleY);
                            DrawFlowArrow(model, new Point(endX, endOffset), arrowSize);
                        }
                        break;
                }

                graphics.DrawLine(GetFlowPen(null, lastSources), endStartPt, endEndPt);
            }
        }

        /// <summary>
        /// Draws an arrow that indicates the flow direction.
        /// </summary>
        /// <param name="model">The flow graph model.</param>
        /// <param name="arrowhead">The position of the arrowhead.</param>
        /// <param name="arrowSize">The size of the arrow, measured from the arrowhead to the opposite side.</param>
        /// <exception cref="ArgumentNullException"><paramref name="model"/> or <paramref name="firstDestinations"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">The model specifies an invalid orientation.</exception>
        private void DrawFlowArrow(FlowLayoutModel<TreeNode> model, Point arrowhead, int arrowSize)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            Point[] arrow;
            switch (model.Orientation)
            {
                case TreeLayoutHelper.Orientation.LeftToRight:
                    arrow = new Point[] {
						arrowhead,
						new Point(arrowhead.X - arrowSize, arrowhead.Y + arrowSize),
						new Point(arrowhead.X - arrowSize, arrowhead.Y - arrowSize)
					};
                    break;
                case TreeLayoutHelper.Orientation.RightToLeft:
                    arrow = new Point[] {
						arrowhead,
						new Point(arrowhead.X + arrowSize, arrowhead.Y + arrowSize),
						new Point(arrowhead.X + arrowSize, arrowhead.Y - arrowSize)
					};
                    break;
                case TreeLayoutHelper.Orientation.TopToBottom:
                    arrow = new Point[] {
						arrowhead,
						new Point(arrowhead.X - arrowSize, arrowhead.Y - arrowSize),
						new Point(arrowhead.X + arrowSize, arrowhead.Y - arrowSize)
					};
                    break;
                case TreeLayoutHelper.Orientation.BottomToTop:
                    arrow = new Point[] {
						arrowhead,
						new Point(arrowhead.X - arrowSize, arrowhead.Y + arrowSize),
						new Point(arrowhead.X + arrowSize, arrowhead.Y + arrowSize)
					};
                    break;
                default:
                    throw new InvalidOperationException("The orientation set for the model is unknown.");
            }

            graphics.FillPolygon(Brushes.Navy, arrow);
            graphics.DrawPolygon(Pens.Navy, arrow);
        }

        /// <summary>
        /// Draws the connections between a given parent node and all of its immediate child nodes.
        /// </summary>
        /// <param name="model">The tree model.</param>
        /// <param name="parent">The parent node.</param>
        /// <param name="children">The child nodes.</param>
        /// <exception cref="ArgumentNullException">Any of the arguments is <see langword="null"/>.</exception>
        public void DrawHierarchyConnections(HierarchyLayoutModel<TreeNode> model, NodeWithBounds<TreeNode> parent, NodeWithBounds<TreeNode>[] children)
        {
            if (children.Length <= 0)
            {
                return;
            }

            var linePen = Pens.Navy;
            int parentLinePos;
            Func<NodeWithBounds<TreeNode>, int> childLinePosFunc;

            switch (model.Orientation)
            {
                case TreeLayoutHelper.Orientation.LeftToRight:
                    {
                        switch (model.ChildAlignment)
                        {
                            case ChildAlignment.Left:
                                parentLinePos = parent.Bottom - Math.Min(parent.Height, model.LevelIndentation) / 2;
                                childLinePosFunc = (child) => child.Bottom;
                                break;
                            case ChildAlignment.Right:
                                parentLinePos = parent.Y + Math.Min(parent.Height, model.LevelIndentation) / 2;
                                childLinePosFunc = (child) => child.Y;
                                break;
                            default:
                                // This should never happen, as the property setter will throw an exception for invalid values.
                                throw new InvalidOperationException("The child alignment was invalid.");
                        }

                        int parentLineEnd = parent.Right;
                        foreach (var child in children)
                        {
                            int childLineX = child.X + child.Width / 2;
                            parentLineEnd = Math.Max(parentLineEnd, childLineX);
                            graphics.DrawLine(linePen,
                                              childLineX, childLinePosFunc(child),
                                              childLineX, parentLinePos);
                        }

                        graphics.DrawLine(linePen,
                                          parent.Right, parentLinePos,
                                          parentLineEnd, parentLinePos);
                    }
                    break;
                case TreeLayoutHelper.Orientation.RightToLeft:
                    {
                        switch (model.ChildAlignment)
                        {
                            case ChildAlignment.Left:
                                parentLinePos = parent.Y + Math.Min(parent.Height, model.LevelIndentation) / 2;
                                childLinePosFunc = (child) => child.Y;
                                break;
                            case ChildAlignment.Right:
                                parentLinePos = parent.Bottom - Math.Min(parent.Height, model.LevelIndentation) / 2;
                                childLinePosFunc = (child) => child.Bottom;
                                break;
                            default:
                                // This should never happen, as the property setter will throw an exception for invalid values.
                                throw new InvalidOperationException("The child alignment was invalid.");
                        }

                        int parentLineEnd = parent.X;
                        foreach (var child in children)
                        {
                            int childLineX = child.X + child.Width / 2;
                            parentLineEnd = Math.Min(parentLineEnd, childLineX);
                            graphics.DrawLine(linePen,
                                              childLineX, childLinePosFunc(child),
                                              childLineX, parentLinePos);
                        }

                        graphics.DrawLine(linePen,
                                          parent.Right, parentLinePos,
                                          parentLineEnd, parentLinePos);
                    }
                    break;
                case TreeLayoutHelper.Orientation.TopToBottom:
                    {
                        switch (model.ChildAlignment)
                        {
                            case ChildAlignment.Left:
                                parentLinePos = parent.X + Math.Min(parent.Width, model.LevelIndentation) / 2;
                                childLinePosFunc = (child) => child.X;
                                break;
                            case ChildAlignment.Right:
                                parentLinePos = parent.Right - Math.Min(parent.Width, model.LevelIndentation) / 2;
                                childLinePosFunc = (child) => child.Right;
                                break;
                            default:
                                // This should never happen, as the property setter will throw an exception for invalid values.
                                throw new InvalidOperationException("The child alignment was invalid.");
                        }

                        int parentLineEnd = parent.Bottom;
                        foreach (var child in children)
                        {
                            int childLineY = child.Y + child.Height / 2;
                            parentLineEnd = Math.Max(parentLineEnd, childLineY);
                            graphics.DrawLine(linePen,
                                              childLinePosFunc(child), childLineY,
                                              parentLinePos, childLineY);
                        }

                        graphics.DrawLine(linePen,
                                          parentLinePos, parent.Bottom,
                                          parentLinePos, parentLineEnd);
                    }
                    break;
                case TreeLayoutHelper.Orientation.BottomToTop:
                    {
                        switch (model.ChildAlignment)
                        {
                            case ChildAlignment.Left:
                                parentLinePos = parent.Right - Math.Min(parent.Width, model.LevelIndentation) / 2;
                                childLinePosFunc = (child) => child.Right;
                                break;
                            case ChildAlignment.Right:
                                parentLinePos = parent.X + Math.Min(parent.Width, model.LevelIndentation) / 2;
                                childLinePosFunc = (child) => child.X;
                                break;
                            default:
                                // This should never happen, as the property setter will throw an exception for invalid values.
                                throw new InvalidOperationException("The child alignment was invalid.");
                        }

                        int parentLineEnd = parent.Y;
                        foreach (var child in children)
                        {
                            int childLineY = child.Y + child.Height / 2;
                            parentLineEnd = Math.Min(parentLineEnd, childLineY);
                            graphics.DrawLine(linePen,
                                              childLinePosFunc(child), childLineY,
                                              parentLinePos, childLineY);
                        }

                        graphics.DrawLine(linePen,
                                          parentLinePos, parent.Bottom,
                                          parentLinePos, parentLineEnd);
                    }
                    break;
            }
        }
        #endregion

        private static readonly Pen FlowUnknown = Pens.Navy;

        private static readonly Pen FlowTrue = new Pen(Color.Green, 2);

        private static readonly Pen FlowFalse = Pens.Red;

        private static Pen GetFlowPen(TreeNode sequenceNode, NodeWithBounds<TreeNode> sourceNode)
        {
            return GetFlowPen(sequenceNode, sourceNode.Node);
        }

        private static Pen GetFlowPen(TreeNode sequenceNode, TreeNode sourceNode)
        {
            var node = FindDirectSequenceChild(sequenceNode, sourceNode);

            if (node.FlowResult().HasValue)
            {
                if (node.FlowResult().Value)
                {
                    return FlowTrue;
                }
                else
                {
                    return FlowFalse;
                }
            }
            else
            {
                return FlowUnknown;
            }
        }

        private static Pen GetFlowPen(TreeNode sequenceNode, IEnumerable<NodeWithBounds<TreeNode>> sourceNodes)
        {
            return GetFlowPen(sequenceNode, sourceNodes.Select(node => node.Node));
        }

        private static Pen GetFlowPen(TreeNode sequenceNode, IEnumerable<TreeNode> sourceNodes)
        {
            var sources = sourceNodes.Select(node => FindDirectSequenceChild(sequenceNode, node)).ToArray();
            if (sources.Length <= 0)
            {
                return FlowUnknown;
            }
            else
            {
                if (sources[0].FlowResult().HasValue)
                {
                    if (sources.Any(node => node.FlowResult().Value))
                    {
                        return FlowTrue;
                    }
                    else
                    {
                        return FlowFalse;
                    }
                }
                else
                {
                    return FlowUnknown;
                }
            }
        }

        private static TreeNode FindDirectSequenceChild(TreeNode sequenceNode, TreeNode child)
        {
            var current = child;
            while (current.Parent != sequenceNode)
            {
                if ((current.Parent != null) && (current.Parent.Text == "or"))
                {
                    break;
                }
                current = current.Parent;
            }
            return current;
        }

        #region complete graphic
        public static int DisplayPadding
        {
            get
            {
                return 10;
            }
        }

        private void DrawGraphicBackground<TCanvas>(LayoutModel<TreeNode, TCanvas> layoutModel)
            where TCanvas : class
        {
            var r = new Rectangle(0, 0, layoutModel.TotalWidth + 2 * DisplayPadding, layoutModel.TotalHeight + 2 * DisplayPadding);
            using (Brush b = new LinearGradientBrush(r,
                                                     Color.White, Color.AliceBlue,
                                                     LinearGradientMode.ForwardDiagonal))
            {
                graphics.FillRectangle(b, r);
            }
        }

        public void InitializeDrawing<TCanvas>(LayoutModel<TreeNode, TCanvas> layoutModel)
            where TCanvas : class
        {
            DrawGraphicBackground(layoutModel);
            graphics.TranslateTransform(DisplayPadding, DisplayPadding);
        }

        public void FinalizeDrawing<TCanvas>(LayoutModel<TreeNode, TCanvas> layoutModel)
            where TCanvas : class
        {
            graphics.ResetTransform();
        }
        #endregion
    }

    public static class Extensions
    {
        public static bool? EvaluationResult(this TreeNode node)
        {
            return false;
        }
        public static bool? FlowResult(this TreeNode node)
        {
            return false;
        }

    }

    #region test

    //private void Form1_Load(object sender, EventArgs e)
    //     {


    //     }

    //     private void panel1_Paint(object sender, PaintEventArgs e)
    //     {
    //          TreeNode node = new TreeNode("广联达");
    //         TreeNode zb = new TreeNode("总部");
    //         zb.Nodes.Add("行政");
    //         zb.Nodes.Add("总裁办");
    //         zb.Nodes.Add("高管");
    //         node.Nodes.Add(zb);

    //         node.Nodes.Add("后勤");
    //         TreeNode sc = new TreeNode("实创");
    //         sc.Nodes.Add("开发");
    //         sc.Nodes.Add("测试");
    //         sc.Nodes.Add("光迅通");
    //         node.Nodes.Add(sc);
    //         node.Nodes.Add("司机");

    //         TreePaint p = new TreePaint(node);
    //         p.SetOrientation(TreeLayoutHelper.Orientation.TopToBottom);
    //         p.DrawTree(e.Graphics);
    //     }
    #endregion
}
