export class Applicant {
    constructor(
        public id: number,
        public firstName: string,
        public lastName: string,
        public email: string,
        public contactNumber: string,
        public currentPosition: string,
        public currentCompany: string,
        public yearsITExperience: string,
        public yearsForSpecificSkills: string,
        public primarySkillId: number,
        public levelId: number,
        public status: number,
        public noticePeriod: string,
        public pendingApplication: string,
        public hiredDate?: Date
    ) { }
}