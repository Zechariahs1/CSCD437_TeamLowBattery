#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <regex.h>
#include <ctype.h>
#include <sys/stat.h>
#include <crypt.h>
#include <time.h>

/* Zechariah Speer 
* Fletcher Baker
* Dillon Geier
*/

void strip(char *array){
	if(array != NULL){
		
		int len = strlen(array), x = 0;
   
		while(array[x] != '\0' && x < len){
	  		if(array[x] == '\r')
		 		array[x] = '\0';

	  		else if(array[x] == '\n')
		 		array[x] = '\0';

	  		x++;

		}// end while
	}//end if
}// end strip


int validateRegex(char * input, char * regExpress){
	regex_t reg;
	int ifVal = 0;
	int valid = regcomp(&reg, regExpress,REG_EXTENDED);
	if(valid == 0){
		valid = regexec(&reg,input,0,NULL,0);
		if( valid == REG_NOMATCH){
			
			ifVal = 0;
		}
		else if(valid == 0 ){
		
			ifVal = 1;
		}
	}
	return ifVal;
}//end validateName

// This method will get the names
int getNameInput(char * temp, char * whichName){
	printf("Please Enter Your %s Name(Only letters of a-z upper or lower case): ",whichName);
	fgets(temp,50,stdin);
	return validateRegex(temp,"^([A-Za-z]){1,50}"); //passing to validate
}//end getNameInput

//This gets a password.
void getPassInput(char * pw)
{
	printf("Password must be between 12-128 characters, and contain at least 1 of each of the following: uppercase letter, lowercase letter, number, symbol.");
	char * regexs[] = {"^[!-~]*([A-Z]){1}[!-~]*", "^[!-~]*([a-z]){1}[!-~]*", "^[!-~]*([0-9]){1}[!-~]*", "^[!-~]*([!-/:-@\\[-`\\{-~]){1}[!-~]*"};
	int i, check;
	do
	{
		printf("\nPassword: ");
		fgets(pw, 128, stdin);
		check = validateRegex(pw, "^([!-~]){12,128}");
		for(i = 0; i<4; i++)
		{
			if(check == 0)
				break;
			check = validateRegex(pw, regexs[i]);
		}
	}while(check == 0);
}//end getPassInput

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

//checks if file exists
int fileExist(char *filename){
	struct stat buffer;
	return (stat(filename,&buffer) == 0);
}//end fileExist

// This method will get the text files names
int getFileInput(char * filein, char * fileout, char * wayToWrite){
	char reg[] ="^[A-Za-z0-9\\!\\#\\-\\+\\.\\;\\=\\@\\-\\`\\{\\}\\~]{1,255}[\\.][txt]";
	char s[260];
	char temp[260];
	char inputOnWayToWrite[3];
	int valid = 0;
	//gets input file
	printf("Please Enter The in Input Filename(Must end in .txt): ");
	fgets(s,260,stdin);
	strip(s);
	
	//gets output file
	printf("Please Enter The in Output Filename(Must end in .txt): ");
	fgets(temp,260,stdin);
	strip(temp);
	if(fileExist(s)){
		if(validateRegex(s, reg) && validateRegex(temp,reg)){
			filein = (char*) malloc((strlen(s) + 1)* sizeof(char));
			strncpy(filein,s,strlen(s));
			fileout = (char*) malloc((strlen(s) + 1)* sizeof(char));
			strncpy(fileout,s,strlen(s));
			if(fileExist(temp)){
				printf("This Output File Exist would you like to append it?(Y = append to file/N = will overwrite the file)");
				fgets(inputOnWayToWrite, 3, stdin);
				if(validateRegex(inputOnWayToWrite, "^[(Y|y|N|n)].")){
					if(inputOnWayToWrite[0] == 'Y' || inputOnWayToWrite[0] == 'y')
						*wayToWrite = 'a';
				}
			}
			if(filein != NULL && fileout != NULL)
				valid = 1;
		}
	}
	return valid;
}//end getNameInput

//THIS IS STILL JUST TEST CODE
int getPassword(char * password)
{
	char * query;
	char pHold[140];
	
	unsigned long seed[2];
	char salt[] = "$1$........";
	const char *const seedChars = ".!?-+=@ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
	int i, len, check;

	seed[0] = time(NULL);
	seed[1] = getpid() ^ (seed[0] >> 14 & 0x30000);

	for(i = 0; i < 8; i++)
		salt[3+i] = seedChars[(seed[i/5] >> i%5*6) & 0x3f];

	getPassInput(pHold);
	query = crypt(pHold, salt);
	len = strlen(query);
	for(i = 0; i < len; i++)
		password[i] = query[i];
	do
	{
		printf("Confirm Password: ");
		fgets(pHold, 128, stdin);
		query = crypt(pHold, password);
		len = strlen(query);
		check = 0;
		for(i = 0; i < len; i++)
		{
			if(query[i] != password[i])
			{
				printf("Password does not match.");
				check =1;
				break;
			}
		}
	}while(check != 0);
	printf("Passwords match.\n");
	return 0;
}//end getPassword

/*
*  This is the Overall function calling all the others
*/
void theDefender(){
	char fname[50];
	char lname[50];
	int numOne,numTwo;
	int completeInput = 0;
	char * infile = NULL, * outfile = NULL;
	char password[128];
	
	char typeOfWrite = 'w';
	
	//will get Name inputs
	do{
		completeInput = getNameInput(fname, "First");
		if(completeInput != 0){
			do{
			completeInput = getNameInput(lname, "Last");
			}while(completeInput == 0);
		}
	}while(completeInput == 0);
	
	completeInput = 0;
	
	//will get int inputs
	do{
		completeInput = getIntInput( &numOne, "First");
		if(completeInput != 0)
			do{
			completeInput = getIntInput( &numTwo,"Second");\
			}while(completeInput == 0);
	}while(completeInput == 0);
	
	completeInput = 0;
	
	//will get text file inputs
	do{
		completeInput = getFileInput(infile, outfile, &typeOfWrite);
	}while(completeInput == 0);
	
	completeInput = 0;

	//will get password and validate
	do
	{
		completeInput = getPassword(password);
	}while(completeInput != 0);
	
	//<<TODO OUTPUT Results to Outputs file>>
	if(infile != NULL && outfile != NULL){
		
		
	}//end if
	
}//end theDefender

int main(){
	theDefender();
	
	return 0;
}//End  main
