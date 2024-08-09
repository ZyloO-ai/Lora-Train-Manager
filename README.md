# ZyloO's LoRA Train Manager
![mockap](/media/mockap.png)

## 📜 Overview
### This application aims to simplify the process of managing LoRAs and datasets.

## ✅ Features

- Tree View of Dataset Folders: Visualize your dataset folders in a tree structure.
- Folder Status View: Track the status of your folders (Pending - Done - Retrain).
- Dataset Information: Get details about the dataset (Image count, has captions, has model).
- Create New Dataset Folder: Automatically create a new dataset folder with img, model, and log subfolders.
- Delete Folders: Easily delete unnecessary folders.
- Open Selected Folder: Quickly open any selected folder.
- Copy Folder Path: Copy the path of the selected folder to your clipboard.
- Open Kohya_ss: Launch Kohya_ss (requires folder selection on the first run).
- Open Tagger: Launch BooruDatasetTagManager (requires folder selection on the first run).
- Calculate Steps: Calculate and display the number of repetitions and epochs, with an option to update the dataset folder name.
- Change Root of All Datasets: Change the root directory of all datasets (warning: this will delete saved states of the datasets).

## 🪛 How It Works

When you launch the software for the first time, you will be prompted to select your current datasets folder. If you do not have one, create an empty folder and select it.

The management and structure verification functions based on the following folder structure:
```bash
  - Root
   - dataset_folder
     - img
     - log
     - model
```
The program only detects valid dataset folders containing img, log, and model subfolders, allowing for better organization.

The program's structure and data are saved in AppData\Roaming\LoraTrainManager\config.json.

## 🌟 Acknowledgements

 - [Kohya_ss](https://github.com/bmaltais/kohya_ss)
 - [BooruDatasetTagManager](https://github.com/starik222/BooruDatasetTagManager)
