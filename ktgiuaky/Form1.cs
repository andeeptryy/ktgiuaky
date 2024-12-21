using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ktgiuaky.Models;

namespace ktgiuaky
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Model1 context = new Model1();
                List<LoaiSP> listLoaiSPs = context.LoaiSPs.ToList(); 
                List<Sanpham> listSanpham = context.Sanphams.ToList(); 
                FillFalcultyCombobox(listLoaiSPs);
                BindGrid(listSanpham);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FillFalcultyCombobox(List<LoaiSP> listLoaiSPs)
        {
            comboBox1.DataSource = listLoaiSPs;
            comboBox1.DisplayMember = "TenLoai";
            comboBox1.ValueMember = "MaLoai";
        }
        
        private void BindGrid(List<Sanpham> listSanpham)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in listSanpham)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item.MaSP;
                dataGridView1.Rows[index].Cells[1].Value = item.TenSP;
                dataGridView1.Rows[index].Cells[2].Value = item.NgayNhap;
                dataGridView1.Rows[index].Cells[3].Value = item.LoaiSP.TenLoai;
            
        }
    }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu hàng được chọn hợp lệ
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Gán giá trị từ hàng được chọn vào các Control
                textBox1.Text = selectedRow.Cells[0].Value?.ToString();
                textBox2.Text = selectedRow.Cells[1].Value?.ToString();
                dateTimePicker1.Value = selectedRow.Cells[2].Value != null
                    ? DateTime.Parse(selectedRow.Cells[2].Value.ToString())
                    : DateTime.Now;
                comboBox1.Text = selectedRow.Cells[3].Value?.ToString();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new Model1())
                {
                    var newSanpham = new Sanpham
                    {
                        MaSP = textBox1.Text,
                        TenSP = textBox2.Text,
                        NgayNhap = dateTimePicker1.Value,
                        MaLoai = comboBox1.SelectedValue.ToString()
                    };

                    context.Sanphams.Add(newSanpham);
                    context.SaveChanges();
                    BindGrid(context.Sanphams.ToList());
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }
    }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new Model1())
                {
                    var maSP = textBox1.Text;
                    var sanpham = context.Sanphams.FirstOrDefault(sp => sp.MaSP == maSP);

                    if (sanpham != null)
                    {
                        sanpham.TenSP = textBox2.Text;
                        sanpham.NgayNhap = dateTimePicker1.Value;
                        sanpham.MaLoai = comboBox1.SelectedValue.ToString();

                        context.SaveChanges();
                        BindGrid(context.Sanphams.ToList());
                        MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sản phẩm để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }
    }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new Model1())
                {
                    var maSP = textBox1.Text;
                    var sanpham = context.Sanphams.FirstOrDefault(sp => sp.MaSP == maSP);

                    if (sanpham != null)
                    {
                        context.Sanphams.Remove(sanpham);
                        context.SaveChanges();
                        BindGrid(context.Sanphams.ToList());
                        MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sản phẩm để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        
    

        }

        private bool isEditing = false;
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new Model1())
                {
                    if (isEditing)
                    {
                       
                        var maSP = textBox1.Text;
                        var sanpham = context.Sanphams.FirstOrDefault(sp => sp.MaSP == maSP);

                        if (sanpham != null)
                        {
                            sanpham.TenSP = textBox2.Text;
                            sanpham.NgayNhap = dateTimePicker1.Value;
                            sanpham.MaLoai = comboBox1.SelectedValue.ToString();
                        }
                    }
                    else
                    {
                       
                        var newSanpham = new Sanpham
                        {
                            MaSP = textBox1.Text,
                            TenSP = textBox2.Text,
                            NgayNhap = dateTimePicker1.Value,
                            MaLoai = comboBox1.SelectedValue.ToString()
                        };

                        context.Sanphams.Add(newSanpham);
                    }

                 
                    context.SaveChanges();
                    BindGrid(context.Sanphams.ToList());
                    MessageBox.Show("Lưu thay đổi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    isEditing = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new Model1())
                {
                   
                    BindGrid(context.Sanphams.ToList());
                    ClearInputs();
                    MessageBox.Show("Hủy thay đổi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isEditing = false; 
                }
                }
                catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void ClearInputs()
        {
            textBox1.Clear();
            textBox2.Clear();
            dateTimePicker1.Value = DateTime.Now;
            comboBox1.SelectedIndex = 0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = textBox3.Text.Trim().ToLower(); 
                using (Model1 context = new Model1())
                {
                   
                    List<Sanpham> filteredList = context.Sanphams
                        .Where(sp => sp.TenSP.ToLower().Contains(keyword))
                        .ToList();

                    if (filteredList.Count > 0)
                    {
                        
                        BindGrid(filteredList);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sản phẩm nào phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
    }
    }
}
