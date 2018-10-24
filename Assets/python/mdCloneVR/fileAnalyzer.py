import json
import random
analyzed_file = []
parameters_dict = {}
person_list = []
curr_person = 0
curr_parameter = 0


def separate_first_line(path):
    """
    separate the first line from the rest of the lines
    :param: path: path of the file you wish to analyze
    :return: first_line: the parameters of the table
             data_lines: the rest of the lines which have the data in them
    :rtype: first_line: list
            data_lines: list
    """
    data_lines = []
    count = 0
    #  open file:
    with open(path, "r") as file:
        #  put all the lines in data_lines
        lines = file.readlines()
        data_lines.append(lines[0])
        lines = lines[1:]
        if len(lines) > 1000:
            for line in lines:
                if 1 - (2000 / len(lines)) < random.uniform(0, 1):
                    data_lines.append(line)
        else:
            data_lines = lines
    #  for each line in data_lines
    for line in data_lines:
        #  delete "\n" for the end of the line:
        data_lines[count] = line[:-1]
        count += 1
    #  put the first line in first_line:
    first_line = [data_lines[0]]
    #  delete the first line from data_lines
    if len(lines) > 1000:
        data_lines = data_lines[1:]
    return first_line, data_lines


def add_parameter_data_to_list(parameter, data_lines):
    """
    put parameter data as a new list in parameter_list without duplicates
    :param data_lines: data of all the people
    """
    global parameters_dict
    global curr_parameter
    parameters_dict[parameter] = []
    for person in data_lines:
        person = person.split(",")
        if not any(person[curr_parameter] in sublist for sublist in parameters_dict[parameter]):
            parameters_dict[parameter].append(person[curr_parameter])
            parameters_dict[parameter].sort()
    curr_parameter += 1


def add_person_data_to_list(person, first_line):
    """
    put person data as a new list in person_list
    :param data_lines: data of all the people
    """
    global person_list
    global curr_person
    count = 0
    person_list.append({})
    person = person.split(",")
    for parameter in first_line:
        person_list[curr_person][parameter] = person[count]
        count += 1

    curr_person += 1


def main(path):
    global analyzed_file
    first_line, data_lines = separate_first_line(path)
    first_line = first_line[0].split(',')
    for parameter in first_line:
        add_parameter_data_to_list(parameter, data_lines)
    for person in data_lines:
        add_person_data_to_list(person, first_line)
    analyzed_file.append([parameters_dict])
    analyzed_file.append(person_list)
    analyzed_file_str = json.dumps(analyzed_file, indent=4)
    try:
        with open(r"C:\Users\cdi2\Downloads\mdClone-20180627T083334Z-001\mdClone\Assets\Resources\data.txt", "w") as f:
            f.write(analyzed_file_str)
    except:
        with open(r"C:\Users\user\UnityProjects\mdClone\Assets\Resources\data.txt", "w") as f:
            f.write(analyzed_file_str)


if __name__ == '__main__':
    main("VrFile3.txt")
