#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <regex.h>
#include <ctype.h>
/* Zechariah Speer
*
*/

int validateRegex(char * input, char * regExpress){
	regex_t reg;
	int valid = regcomp(&reg, regExpress,REG_EXTENDED);
	if(valid == 0){
		valid = regexec(&reg,input,0,NULL,0);
		if( valid == REG_NOMATCH){
			return 0;
		}
		else if(valid == 0 ){
			return 1;
		}
	}
	else{
		return 0;
	}
}//end validateName

// This method will get the names
int getNameInput(char * temp, char * whichName){
	printf("Please Enter Your %s Name(Only letters of a-z upper or lower case): ",whichName);
	fgets(temp,50,stdin);
	return validateRegex(temp,"^([A-Za-z]){1,50}"); //passing to validate
}//end getNameInput

//This gets int input and Validates 
int getIntInput(int * num, char * whichNum){
	char s[65535];
	int valid = 0;
	printf("Please Enter The %s Integer Number: ",whichNum);
	fgets(s,65535,stdin);
	int len = strlen(s);
	
	//this Validation if from https://stackoverflow.com/questions/4072190/check-if-input-is-integer-type-in-c
	while (len > 0 && isspace(s[len - 1]))
		len--;     // strip trailing newline or other white space
	if (len > 0){
		valid = 1;
		for (int i = 0; i < len; ++i){
			if (!isdigit(s[i])){
				valid = 0;
				break;
			}
		}
	}
	if(valid == 1)
		*num = atoi(s);
	return valid;
}//end getIntInput

// This method will get the text files
int getFileInput(FILE * temp, char * whichName , char * howToOpen){
	char s[50];
	int valid = 0;
	printf("Please Enter The %s Filename(Must end in .txt): ",whichName);
	fgets(s,50,stdin);
	printf("%s\n",s);
	if(access(s,F_OK) != -1){
		if(validateRegex(s, "^.\\.(txt)$")){
			temp = fopen(s,howToOpen);
			valid = 1;
		}
	}
	return valid;
}//end getNameInput

/*
*  This is the Overall function calling all the others
*/
void theDefender(){
	char fname[50];
	char lname[50];
	int numOne,numTwo;
	int completeInput = 0;
	FILE * infile, * outfile;
	
	//will get Name inputs
	do{
		completeInput = getNameInput(fname, "First");
		if(completeInput != 0){
			completeInput = getNameInput(lname, "Last");
		}
	}while(completeInput == 0);
	
	completeInput = 0;
	
	//will get int inputs
	do{
		completeInput = getIntInput( &numOne, "First");
		if(completeInput != 0)
			completeInput = getIntInput( &numTwo,"Second");
	}while(completeInput == 0);
	
	completeInput = 0;
	
	//will get text file inputs
	//<<<<<TODO text files still need validating>>>>
	do{
		completeInput = getFileInput(infile, "Input", "r");
		if(completeInput != 0){
			completeInput = getFileInput(infile, "Out", "w");
		}
	}while(completeInput == 0);
	
	//<<TODO PASSWORD and Validations>>
	
	//<<TODO OUTPUT Results to Outputs file>>
	
}//end theDefender

int main(){
	theDefender();
	
	return 0;
}//End  main