using System;
using System.IO;
using UnityEngine;
using Random = System.Random;

public class Car : MonoBehaviour {
    public Transform carPlayer;

    [Header("軌道圖層")]
    public LayerMask trackLineLayer;

    [Header("障礙物圖層")]
    public LayerMask obstacleLayer;

    [Header("軌道感測器")]
    public Transform[] trackLineSensor = new Transform[2];

    [Header("障礙物感測器")]
    public Transform[] obstacleSensor = new Transform[11];

    [Header("軌道與車距離")]
    public float[] tlsDistance = new float[2];

    [Header("障礙物與車距離和速度")]
    public float[] obsDistance = new float[11];
    float[] obsdBackup = new float[11];
    public float[] obsVelocity = new float[11];

    [Header("目前的評價函數")]
    public float currentlyFitnessValue;

    [Header("最佳化方法執行次數")]
    public int FES;

    /// <summary>軌道偵測</summary>
    void TrackLineCheck() {
        Vector2 tlSensor;
        Vector2 tlSensorVector;

        tlSensor = trackLineSensor[0].position;
        tlSensorVector = Quaternion.Euler(0, 0, trackLineSensor[0].rotation.eulerAngles.z) * new Vector2(0.5f, 0);
        if (Physics2D.Linecast(tlSensor, tlSensor + tlSensorVector, trackLineLayer)) {
            tlsDistance[0] = Physics2D.Linecast(tlSensor, tlSensor + tlSensorVector, trackLineLayer).distance / 0.5f;
        }
        else {
            tlsDistance[0] = 1;
        }
        Debug.DrawLine(tlSensor, tlSensor + tlSensorVector, Color.red);

        tlSensor = trackLineSensor[1].position;
        tlSensorVector = Quaternion.Euler(0, 0, trackLineSensor[1].rotation.eulerAngles.z) * new Vector2(-0.5f, 0);
        if (Physics2D.Linecast(tlSensor, tlSensor + tlSensorVector, trackLineLayer)) {
            tlsDistance[1] = Physics2D.Linecast(tlSensor, tlSensor + tlSensorVector, trackLineLayer).distance / 0.5f;
        }
        else {
            tlsDistance[1] = 1;
        }
        Debug.DrawLine(tlSensor, tlSensor + tlSensorVector, Color.red);
    }

    /// <summary>障礙物偵測</summary>
    void ObstacleCheck() {
        Vector2 obSensor;
        Vector2 obSensorVector;

        obSensor = obstacleSensor[0].position;
        obSensorVector = Quaternion.Euler(0, 0, obstacleSensor[0].rotation.eulerAngles.z) * new Vector2(0, 1);
        if (Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer)) {
            obsDistance[0] = Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer).distance;
        }
        else {
            obsDistance[0] = 1;
        }
        Debug.DrawLine(obSensor, obSensor + obSensorVector, Color.blue);
        obsVelocity[0] = obsdBackup[0] - obsDistance[0];
        obsdBackup[0] = obsDistance[0];

        obSensor = obstacleSensor[1].position;
        obSensorVector = Quaternion.Euler(0, 0, obstacleSensor[1].rotation.eulerAngles.z) * new Vector2(0.5f, 0.866f);
        if (Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer)) {
            obsDistance[1] = Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer).distance;
        }
        else {
            obsDistance[1] = 1;
        }
        Debug.DrawLine(obSensor, obSensor + obSensorVector, Color.blue);
        obsVelocity[1] = obsdBackup[1] - obsDistance[1];
        obsdBackup[1] = obsDistance[1];

        obSensor = obstacleSensor[2].position;
        obSensorVector = Quaternion.Euler(0, 0, obstacleSensor[2].rotation.eulerAngles.z) * new Vector2(0.707f, 0.707f);
        if (Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer)) {
            obsDistance[2] = Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer).distance;
        }
        else {
            obsDistance[2] = 1;
        }
        Debug.DrawLine(obSensor, obSensor + obSensorVector, Color.blue);
        obsVelocity[2] = obsdBackup[2] - obsDistance[2];
        obsdBackup[2] = obsDistance[2];

        obSensor = obstacleSensor[3].position;
        obSensorVector = Quaternion.Euler(0, 0, obstacleSensor[3].rotation.eulerAngles.z) * new Vector2(0.866f, 0.5f);
        if (Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer)) {
            obsDistance[3] = Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer).distance;
        }
        else {
            obsDistance[3] = 1;
        }
        Debug.DrawLine(obSensor, obSensor + obSensorVector, Color.blue);
        obsVelocity[3] = obsdBackup[3] - obsDistance[3];
        obsdBackup[3] = obsDistance[3];

        obSensor = obstacleSensor[4].position;
        obSensorVector = Quaternion.Euler(0, 0, obstacleSensor[4].rotation.eulerAngles.z) * new Vector2(0.866f, -0.5f);
        if (Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer)) {
            obsDistance[4] = Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer).distance;
        }
        else {
            obsDistance[4] = 1;
        }
        Debug.DrawLine(obSensor, obSensor + obSensorVector, Color.blue);
        obsVelocity[4] = obsdBackup[4] - obsDistance[4];
        obsdBackup[4] = obsDistance[4];

        obSensor = obstacleSensor[5].position;
        obSensorVector = Quaternion.Euler(0, 0, obstacleSensor[5].rotation.eulerAngles.z) * new Vector2(0.5f, -0.866f);
        if (Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer)) {
            obsDistance[5] = Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer).distance;
        }
        else {
            obsDistance[5] = 1;
        }
        Debug.DrawLine(obSensor, obSensor + obSensorVector, Color.blue);
        obsVelocity[5] = obsdBackup[5] - obsDistance[5];
        obsdBackup[5] = obsDistance[5];

        obSensor = obstacleSensor[6].position;
        obSensorVector = Quaternion.Euler(0, 0, obstacleSensor[6].rotation.eulerAngles.z) * new Vector2(-0.5f, -0.866f);
        if (Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer)) {
            obsDistance[6] = Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer).distance;
        }
        else {
            obsDistance[6] = 1;
        }
        Debug.DrawLine(obSensor, obSensor + obSensorVector, Color.blue);
        obsVelocity[6] = obsdBackup[6] - obsDistance[6];
        obsdBackup[6] = obsDistance[6];

        obSensor = obstacleSensor[7].position;
        obSensorVector = Quaternion.Euler(0, 0, obstacleSensor[7].rotation.eulerAngles.z) * new Vector2(-0.866f, -0.5f);
        if (Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer)) {
            obsDistance[7] = Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer).distance;
        }
        else {
            obsDistance[7] = 1;
        }
        Debug.DrawLine(obSensor, obSensor + obSensorVector, Color.blue);
        obsVelocity[7] = obsdBackup[7] - obsDistance[7];
        obsdBackup[7] = obsDistance[7];

        obSensor = obstacleSensor[8].position;
        obSensorVector = Quaternion.Euler(0, 0, obstacleSensor[8].rotation.eulerAngles.z) * new Vector2(-0.866f, 0.5f);
        if (Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer)) {
            obsDistance[8] = Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer).distance;
        }
        else {
            obsDistance[8] = 1;
        }
        Debug.DrawLine(obSensor, obSensor + obSensorVector, Color.blue);
        obsVelocity[8] = obsdBackup[8] - obsDistance[8];
        obsdBackup[8] = obsDistance[8];

        obSensor = obstacleSensor[9].position;
        obSensorVector = Quaternion.Euler(0, 0, obstacleSensor[9].rotation.eulerAngles.z) * new Vector2(-0.707f, 0.707f);
        if (Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer)) {
            obsDistance[9] = Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer).distance;
        }
        else {
            obsDistance[9] = 1;
        }
        Debug.DrawLine(obSensor, obSensor + obSensorVector, Color.blue);
        obsVelocity[9] = obsdBackup[9] - obsDistance[9];
        obsdBackup[9] = obsDistance[9];

        obSensor = obstacleSensor[10].position;
        obSensorVector = Quaternion.Euler(0, 0, obstacleSensor[10].rotation.eulerAngles.z) * new Vector2(-0.5f, 0.866f);
        if (Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer)) {
            obsDistance[10] = Physics2D.Linecast(obSensor, obSensor + obSensorVector, obstacleLayer).distance;
        }
        else {
            obsDistance[10] = 1;
        }
        Debug.DrawLine(obSensor, obSensor + obSensorVector, Color.blue);
        obsVelocity[10] = obsdBackup[10] - obsDistance[10];
        obsdBackup[10] = obsDistance[10];
    }

    //卷積類神經網路
    float[,] originalDataL1 = new float[5, 5];
    float[,,] kernelMapsL2 = new float[6, 3, 3];
    float[,,] kernelMapsOutputL2 = new float[6, 3, 3];
    float[,,] kernelMapsL3 = new float[10, 2, 4];
    float[,,] kernelMapsOutputL3 = new float[10, 2, 2];
    float[] poolingOutputL4 = new float[10];
    float[,] hiddenLayerL5 = new float[100, 10];
    float[] hiddenLayerOutputL5 = new float[100];
    float[,] outputLayerL6 = new float[8, 100];
    float[] outputLayerOutputL6 = new float[8];

    /// <summary>初始類神經網路</summary>
    void InitialNeuralNetwork(float[] temp_element) {
        int elementCount = 0;
        for (int i = 0; i < 6; i++) {
            for (int j = 0; j < 3; j++) {
                for (int k = 0; k < 3; k++) {
                    kernelMapsL2[i, j, k] = temp_element[elementCount];
                    elementCount++;
                }
            }
        }
        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 2; j++) {
                for (int k = 0; k < 4; k++) {
                    kernelMapsL3[i, j, k] = temp_element[elementCount];
                    elementCount++;
                }
            }
        }
        for (int i = 0; i < 100; i++) {
            for (int j = 0; j < 10; j++) {
                hiddenLayerL5[i, j] = temp_element[elementCount];
                elementCount++;
            }
        }
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 100; j++) {
                outputLayerL6[i, j] = temp_element[elementCount];
                elementCount++;
            }
        }
    }

    /// <summary>車輛移動</summary>
    void CarMovement() {
        TrackLineCheck();
        ObstacleCheck();
        if (tlsDistance[0] != 0 && tlsDistance[1] != 0
            && obsDistance[0] != 0
            && obsDistance[1] != 0 && obsDistance[2] != 0 && obsDistance[3] != 0
            && obsDistance[4] != 0 && obsDistance[5] != 0 && obsDistance[6] != 0 && obsDistance[7] != 0
            && obsDistance[8] != 0 && obsDistance[9] != 0 && obsDistance[10] != 0) {

            //初始Data
            originalDataL1[0, 0] = tlsDistance[0];
            originalDataL1[0, 1] = tlsDistance[1];
            originalDataL1[0, 2] = 0;
            originalDataL1[0, 3] = obsDistance[0];
            originalDataL1[0, 4] = obsDistance[1];
            originalDataL1[1, 0] = obsDistance[2];
            originalDataL1[1, 1] = obsDistance[3];
            originalDataL1[1, 2] = obsDistance[4];
            originalDataL1[1, 3] = obsDistance[5];
            originalDataL1[1, 4] = obsDistance[6];
            originalDataL1[2, 0] = obsDistance[7];
            originalDataL1[2, 1] = obsDistance[8];
            originalDataL1[2, 2] = obsDistance[9];
            originalDataL1[2, 3] = obsDistance[10];
            originalDataL1[2, 4] = obsVelocity[0];
            originalDataL1[3, 0] = obsVelocity[1];
            originalDataL1[3, 1] = obsVelocity[2];
            originalDataL1[3, 2] = obsVelocity[3];
            originalDataL1[3, 3] = obsVelocity[4];
            originalDataL1[3, 4] = obsVelocity[5];
            originalDataL1[4, 0] = obsVelocity[6];
            originalDataL1[4, 1] = obsVelocity[7];
            originalDataL1[4, 2] = obsVelocity[8];
            originalDataL1[4, 3] = obsVelocity[9];
            originalDataL1[4, 4] = obsVelocity[10];

            //L1 Kernel Maps Output
            for (int i = 0; i < 6; i++) {
                for (int j = 0; j < 3; j++) {
                    for (int k = 0; k < 3; k++) {
                        kernelMapsOutputL2[i, j, k] = 0;
                        for (int l = 0; l < 3; l++) {
                            for (int m = 0; m < 3; m++) {
                                kernelMapsOutputL2[i, j, k] += kernelMapsL2[i, l, m] * originalDataL1[j + l, k + m];
                            }
                        }
                        if (kernelMapsOutputL2[i, j, k] < 0) {
                            kernelMapsOutputL2[i, j, k] = 0;
                        }
                    }
                }
            }

            //L2 Kernel Maps Output
            int[,] kernelpair = new int[,] { { 5, 3 }, { 4, 0 }, { 2, 1 }, { 5, 4 }, { 3, 0 }, { 4, 2 }, { 0, 1 }, { 3, 2 }, { 5, 1 }, { 0, 5 } };
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 2; j++) {
                    for (int k = 0; k < 2; k++) {
                        kernelMapsOutputL3[i, j, k] = 0;
                        for (int l = 0; l < 2; l++) {
                            for (int m = 0; m < 2; m++) {
                                kernelMapsOutputL3[i, j, k] += kernelMapsL3[i, l, m] * kernelMapsOutputL2[kernelpair[i, 0], j + l, k + m]
                                    + kernelMapsL3[i, l, m + 2] * kernelMapsOutputL2[kernelpair[i, 1], j + l, k + m];
                            }
                        }
                        if (kernelMapsOutputL3[i, j, k] < 0) {
                            kernelMapsOutputL3[i, j, k] = 0;
                        }
                    }
                }
            }

            //L3 Pooling Output
            for (int i = 0; i < 10; i++) {
                poolingOutputL4[i] = kernelMapsOutputL3[i, 0, 0];
                for (int j = 0; j < 2; j++) {
                    for (int k = 0; k < 2; k++) {
                        if (kernelMapsOutputL3[i, j, k] > poolingOutputL4[i]) {
                            poolingOutputL4[i] = kernelMapsOutputL3[i, j, k];
                        }
                    }
                }
            }

            //Hidden Layer Output
            for (int i = 0; i < 100; i++) {
                hiddenLayerOutputL5[i] = 0;
                for (int j = 0; j < 10; j++) {
                    hiddenLayerOutputL5[i] += hiddenLayerL5[i, j] * poolingOutputL4[j];
                }
                hiddenLayerOutputL5[i] = (float)(2 / (1 + Math.Exp(-2 * hiddenLayerOutputL5[i])) - 1);
            }

            //Output Layer Output
            for (int i = 0; i < 8; i++) {
                outputLayerOutputL6[i] = 0;
                for (int j = 0; j < 100; j++) {
                    outputLayerOutputL6[i] += outputLayerL6[i, j] * hiddenLayerOutputL5[j];
                }
                outputLayerOutputL6[i] = (float)(2 / (1 + Math.Exp(-2 * outputLayerOutputL6[i])) - 1);
                if (outputLayerOutputL6[i] > 0) {
                    outputLayerOutputL6[i] = 1;
                }
                else {
                    outputLayerOutputL6[i] = 0;
                }
            }

            //Move Car
            float carForward = 0;
            float carTurn = 0;
            for (int i = 0; i < 4; i++) {
                carForward += (float)(outputLayerOutputL6[i] * Math.Pow(2, i));
            }
            for (int i = 4; i < 8; i++) {
                carTurn += (float)(outputLayerOutputL6[i] * Math.Pow(2, i - 4));
            }
            carForward = (carForward / 15) * 0.04f + 0.01f;
            carTurn = (carTurn / 15) * 4f - 2;
            carPlayer.Translate(new Vector2(0, carForward));
            carPlayer.Rotate(new Vector3(0, 0, carTurn));
            _lp_fv[id] += carForward;
            timeCounter += 0.02f;
            if (obsVelocity[0]
                + obsVelocity[1] + obsVelocity[2] + obsVelocity[3]
                + obsVelocity[8] + obsVelocity[9] + obsVelocity[10] == 0
                && carForward > 0.03f) {
                _lp_fv[id] += carForward;
            }
        }
        else {
            _lp_fv[id] += 1 / timeCounter;
            carEnable = false;
        }
        currentlyFitnessValue = _lp_fv[id];
    }

    //Optimization Method 參數
    Random rand;
    int _particle;
    int _dim;
    int _uplimit;
    int _lowlimit;
    float[,] _lv;
    float[,] _lp;
    float[] _lp_fv;
    float[,] _lpp;
    float[] _lpp_fv;
    int _lgp_id;
    float[] _wc;
    float[] _cc;
    float[] _sm;
    int id;
    int Pm;
    int[] m;
    int[] temp_id;
    bool update_id;
    bool carEnable;
    float timeCounter;

    /// <summary>初始最佳化方法</summary>
    void InitialOptimizationMethod(int _particle) {
        this._particle = _particle;
        _dim = kernelMapsL2.GetLength(0) * kernelMapsL2.GetLength(1) * kernelMapsL2.GetLength(2)
            + kernelMapsL3.GetLength(0) * kernelMapsL3.GetLength(1) * kernelMapsL3.GetLength(2)
            + hiddenLayerL5.GetLength(0) * hiddenLayerL5.GetLength(1)
            + outputLayerL6.GetLength(0) * outputLayerL6.GetLength(1);
        _uplimit = 1;
        _lowlimit = -1;
        _lv = new float[_particle, _dim];
        _lp = new float[_particle, _dim];
        _lp_fv = new float[_particle];
        _lpp = new float[_particle, _dim];
        _lpp_fv = new float[_particle];
        _lgp_id = (int)Math.Floor(_particle * rand.NextDouble());
        _wc = new float[_particle];
        _cc = new float[_particle];
        _sm = new float[_particle];
        id = _particle - 1;
        Pm = 7;
        m = new int[_particle];
        temp_id = new int[_particle];
        update_id = false;
        carEnable = false;

        string[] readCSV = File.ReadAllLines("./Assets/Data/Parameter.csv");
        for (int i = 0; i < _particle; i++) {
            string[] r;
            r = readCSV[(int)Math.Floor(readCSV.Length * rand.NextDouble())].Split(',');
            _wc[i] = float.Parse(r[0]);
            r = readCSV[(int)Math.Floor(readCSV.Length * rand.NextDouble())].Split(',');
            _cc[i] = float.Parse(r[1]);
            r = readCSV[(int)Math.Floor(readCSV.Length * rand.NextDouble())].Split(',');
            _sm[i] = float.Parse(r[2]);
            for (int j = 0; j < _dim; j++) {
                _lv[i, j] = (_uplimit - _lowlimit) * (float)rand.NextDouble() + _lowlimit;
                _lp[i, j] = (_uplimit - _lowlimit) * (float)rand.NextDouble() + _lowlimit;
            }
        }
    }

    /// <summary>每隔一段時間更新所有車輛產生的最佳Neural Network</summary>
    void UpdateBestNeuralsCSV() {
        string[] tempBestNeurals;
        tempBestNeurals = File.ReadAllLines("./Assets/Data/BestNeurals.csv");
        if (_lpp_fv[_lgp_id] > float.Parse(tempBestNeurals[0])) {
            StreamWriter w = new StreamWriter("./Assets/Data/BestNeurals.csv", false);
            w.WriteLine(_lpp_fv[_lgp_id]);
            for (int i = 0; i < _dim; i++) {
                w.WriteLine(_lpp[_lgp_id, i]);
            }
            w.Close();
        }
        else if (_lpp_fv[_lgp_id] < float.Parse(tempBestNeurals[0])) {
            for (int i = 0; i < _dim; i++) {
                _lpp[_lgp_id, i] = float.Parse(tempBestNeurals[i + 1]);
            }
        }
    }

    /// <summary>初始車輛位置</summary>
    void InitialCarPosition() {
        carPlayer.position = new Vector3(-20, -5.776f, -0.1f);
        carPlayer.rotation = Quaternion.Euler(0, 0, 0);
        TrackLineCheck();
        ObstacleCheck();
    }

    // Update
    void FixedUpdate() {
        if (carEnable == false) {
            if (update_id == true) {
                if (_lp_fv[id] > _lpp_fv[temp_id[id]]) {
                    if (_lp_fv[id] > _lpp_fv[_lgp_id]) {
                        _lgp_id = temp_id[id];
                    }
                    _lpp_fv[temp_id[id]] = _lp_fv[id];
                    for (int i = 0; i < _dim; i++) {
                        _lpp[temp_id[id], i] = _lp[id, i];
                    }
                }
                else if (_lp_fv[id] > _lpp_fv[id]) {
                    _lpp_fv[id] = _lp_fv[id];
                    for (int i = 0; i < _dim; i++) {
                        _lpp[id, i] = _lp[id, i];
                    }
                }
                else {
                    m[id]++;
                }
            }
            if (id == _particle - 1) {
                id = 0;
                FES++;
            }
            else {
                id++;
            }
            temp_id[id] = _lgp_id;
            if (m[id] < Pm) {
                while (temp_id[id] == _lgp_id) {
                    temp_id[id] = (int)Math.Floor(_particle * rand.NextDouble());
                }
            }
            else {
                m[id] = 0;
            }
            for (int i = 0; i < _dim; i++) {
                _lv[id, i] = _wc[id] * (float)rand.NextDouble() * _lv[id, i] + _cc[id] * (float)rand.NextDouble() * (_lpp[temp_id[id], i] - _lp[id, i]);
            }
            if ((float)rand.NextDouble() < _sm[id]) {
                _lp[id, (int)Math.Floor(_dim * rand.NextDouble())] = (_uplimit - _lowlimit) * (float)rand.NextDouble() + _lowlimit;
            }
            for (int i = 0; i < _dim; i++) {
                _lp[id, i] += _lv[id, i];
            }
            update_id = true;
            for (int i = 0; i < _dim; i++) {
                if (_lp[id, i] < _lowlimit || _lp[id, i] > _uplimit) {
                    update_id = false;
                    break;
                }
            }
            if (update_id == true) {
                float[] temp_lp = new float[_dim];
                for (int i = 0; i < _dim; i++) {
                    temp_lp[i] = _lp[id, i];
                }
                InitialCarPosition();
                InitialNeuralNetwork(temp_lp);
                _lp_fv[id] = 0;
                timeCounter = 0;
                carEnable = true;
            }
        }
        else {
            CarMovement();
        }
    }

    // Start is called before the first frame update
    void Start() {
        rand = new Random(100 * DateTime.Now.Millisecond);
        InvokeRepeating("UpdateBestNeuralsCSV", 600 * (float)rand.NextDouble() + 300, 600 * (float)rand.NextDouble() + 300);
        InitialOptimizationMethod(20);
    }
}
