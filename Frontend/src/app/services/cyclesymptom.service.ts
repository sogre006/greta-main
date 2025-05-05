import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Symptom } from '../models/Symptom';

export interface CreateCycleSymptomDto {
  cycleId:   number;
  symptomId: number;
  intensity: number;
  date:      string;  // "YYYY-MM-DD"
}

export interface CycleSymptom {
  cycleSymptomId: number;
  cycleId:        number;
  symptomId:      number;
  intensity:      number;
  date:           string;
  createdAt:      string;
  symptom?:       Symptom; 
}

@Injectable({ providedIn: 'root' })
export class CycleSymptomService {
  // Point directly at your backend API
  private baseUrl = 'http://localhost:5113/api/CycleSymptom';

  constructor(private http: HttpClient) {}

  createCycleSymptom(dto: CreateCycleSymptomDto): Observable<CycleSymptom> {
    return this.http.post<CycleSymptom>(this.baseUrl, dto);
  }

  getCycleSymptomsByCycleId(cycleId: number): Observable<CycleSymptom[]> {
    return this.http.get<CycleSymptom[]>(`${this.baseUrl}/cycle/${cycleId}`);
  }
}