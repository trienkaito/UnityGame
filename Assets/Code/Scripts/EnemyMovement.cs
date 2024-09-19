using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    // Rigidbody2D của đối tượng kẻ thù để điều khiển vật lý
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Attributes")]
    // Tốc độ di chuyển của kẻ thù (có thể thay đổi trong Unity Editor)
    [SerializeField] private float moveSpeed = 2f;

    // Biến lưu vị trí mục tiêu tiếp theo mà kẻ thù sẽ di chuyển tới
    private Transform target;
    // Chỉ số đường đi hiện tại (để xác định điểm trong đường đi)
    private int pathIndex = 0;

    // Phương thức này được gọi khi kẻ thù khởi tạo
    private void Start()
    {
        // Lấy điểm đầu tiên trong đường đi từ LevelManager
        target = LevelManager.main.path[pathIndex];
    }

    // Phương thức Update() được gọi mỗi khung hình
    private void Update()
    {
        // Kiểm tra khoảng cách giữa vị trí hiện tại và mục tiêu
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            // Tăng chỉ số đường đi để chuyển sang điểm tiếp theo
            pathIndex++;
            // Nếu đã đạt đến cuối đường đi, hủy đối tượng kẻ thù
            if (pathIndex == LevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                // Cập nhật mục tiêu thành điểm tiếp theo
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    // Phương thức FixedUpdate() được gọi mỗi frame vật lý
    private void FixedUpdate()
    {
        // Tính toán hướng từ vị trí hiện tại tới mục tiêu
        Vector2 direction = (target.position - transform.position).normalized;

        // Cập nhật vận tốc của Rigidbody2D theo hướng đã tính toán và tốc độ di chuyển
        rb.velocity = direction * moveSpeed;
    }
}
